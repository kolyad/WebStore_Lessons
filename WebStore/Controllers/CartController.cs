using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View(new CartOrderViewModel { Cart = _cartService.GetViewModel() });
        }

        public IActionResult Add(int id)
        {
            _cartService.Add(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrement(int id)
        {
            _cartService.Decrement(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _cartService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _cartService.Clear();
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        [ActionName("CheckOut")]
        public async Task<IActionResult> CheckOutAsync(OrderViewModel orderModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _cartService.GetViewModel(),
                    Order = orderModel
                });
            }

            var order = await orderService.CreatOrderAsync(
                User.Identity.Name,
                _cartService.GetViewModel(),
                orderModel
                );

            _cartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });

        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
