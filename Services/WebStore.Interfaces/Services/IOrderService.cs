using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userName);

        Task<Order> GetOrderByIdAsync(int id);

        Task<Order> CreatOrderAsync(string userName, CartViewModel cartModel, OrderViewModel orderModel);

        Task<IEnumerable<Product>> GetUsedProductsAsync();
    }
}
