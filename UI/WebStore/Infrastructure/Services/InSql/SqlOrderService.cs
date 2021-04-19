using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Infrastructure.Interfaces;
using WebStore.Domain.ViewModels;

namespace WebStore.Infrastructure.Services.InSql
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

        public async Task<Order> CreatOrderAsync(string userName, CartViewModel cartModel, OrderViewModel orderModel)
        {
            var user = await _userManager.FindByNameAsync(userName);
            _ = user ?? throw new InvalidOperationException($"Пользователь {userName} не найден в БД");

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            {
                Name = orderModel.Name,
                Address = orderModel.Address,
                Phone = orderModel.Phone,
                User = user,
            };

            var product_ids = cartModel.Items.Select(x => x.Product.Id).ToArray();

            var cart_products = await _db.Products.Where(x => product_ids.Contains(x.Id)).ToArrayAsync();

            order.Items = cartModel.Items.Join(
                cart_products,
                cart_item => cart_item.Product.Id,
                product => product.Id,
                (cart_item, product) => new OrderItem
                {
                    Order = order,
                    Product = product,
                    Price = product.Price,
                    Quantity = cart_item.Quantity
                })
                .ToArray();

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _db.Orders
                .Include(x => x.User)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetUsedProductsAsync()
        {
            return await _db.OrderItems
                .Select(x => x.Product)
                .Distinct()
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userName)
        {
            return await _db.Orders
                .Include(x => x.User)
                .Include(x => x.Items)
                .Where(x => x.User.UserName == userName)
                .ToArrayAsync();
        }
    }
}
