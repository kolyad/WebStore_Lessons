using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.Domain.ViewModels;

namespace WebStore.Infrastructure.Services.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductData _productData;
        private readonly string _cartName;

        private Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context!.Response.Cookies;
                var cart_cookies = context.Request.Cookies[_cartName];
                if (cart_cookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }
                ReplaceCookies(cookies, cart_cookies);
                return JsonConvert.DeserializeObject<Cart>(cart_cookies);
            }

            set => ReplaceCookies(_httpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookie);
        }

        public InCookiesCartService(
            IHttpContextAccessor httpContextAccessor,
            IProductData productData)
        {
            _httpContextAccessor = httpContextAccessor;
            _productData = productData;

            var user = _httpContextAccessor.HttpContext.User;
            var userName = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

            _cartName = $"WebStore.Cart{userName}";
        }

        public void Add(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item is null)
            {
                cart.Items.Add(new CartItem { ProductId = id });
            }
            else
            {
                item.Quantity++;
            }

            Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item is null)
            {
                return;
            }

            if (item.Quantity > 0)
            {
                item.Quantity--;
            }
            
            if (item.Quantity == 0)
            {
                cart.Items.Remove(item);
            }

            Cart = cart;
        }

        public void Delete(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item is null)
            {
                return;
            }

            cart.Items.Remove(item);

            Cart = cart;
        }

        public void Clear()
        {
            var cart = Cart;           

            cart.Items.Clear();

            Cart = cart;
        }


        public CartViewModel GetViewModel()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(x => x.ProductId).ToArray()
            });

            var productViewModels = products.ToView().ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = Cart.Items
                .Where(i => productViewModels.ContainsKey(i.ProductId))
                .Select(x => (productViewModels[x.ProductId], x.Quantity))
            };

        }
    }
}
