using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.InSql
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDb _db;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreDb db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<OrderDto> CreatOrderAsync(string userName, CreateOrderModel orderModel)
        {
            var user = await _userManager.FindByNameAsync(userName);
            _ = user ?? throw new InvalidOperationException($"Пользователь {userName} не найден в БД");

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            {
                Name = orderModel.Order.Name,
                Address = orderModel.Order.Address,
                Phone = orderModel.Order.Phone,
                User = user,
            };

            //var product_ids = cartModel.Items.Select(x => x.Product.Id).ToArray();

            //var cart_products = await _db.Products.Where(x => product_ids.Contains(x.Id)).ToArrayAsync();

            //order.Items = cartModel.Items.Join(
            //    cart_products,
            //    cart_item => cart_item.Product.Id,
            //    product => product.Id,
            //    (cart_item, product) => new OrderItem
            //    {
            //        Order = order,
            //        Product = product,
            //        Price = product.Price,
            //        Quantity = cart_item.Quantity
            //    })
            //    .ToArray();

            foreach (var item in orderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.Id);
                if (product is null)
                {
                    continue;
                }

                var orderItem = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = product,
                };

                order.Items.Add(orderItem);
            }

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order.ToDto();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            return (await _db.Orders
                .Include(x => x.User)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id))
                .ToDto();
        }

        public async Task<IEnumerable<ProductDto>> GetUsedProductsAsync()
        {
            return (await _db.OrderItems
                .Select(x => x.Product)
                .Distinct()
                .ToArrayAsync())
                .ToDto();
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userName)
        {
            return (await _db.Orders
                .Include(x => x.User)
                .Include(x => x.Items)
                .Where(x => x.User.UserName == userName)
                .ToArrayAsync())
                .Select(o => o.ToDto());
        }
    }
}
