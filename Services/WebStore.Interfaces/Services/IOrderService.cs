using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Dto;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userName);

        Task<OrderDto> GetOrderByIdAsync(int id);

        Task<OrderDto> CreatOrderAsync(string userName, CreateOrderModel createOrderModel);

        Task<IEnumerable<ProductDto>> GetUsedProductsAsync();
    }
}
