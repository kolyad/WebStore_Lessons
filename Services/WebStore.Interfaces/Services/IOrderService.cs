using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Dto;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userName);

        Task<OrderDto> GetOrderByIdAsync(int id);

        Task<OrderDto> CreateOrderAsync(string userName, CreateOrderModel createOrderModel);

        Task<IEnumerable<ProductDto>> GetUsedProductsAsync();
    }
}
