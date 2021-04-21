using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Dto;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Clients
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, WebApi.Orders)
        {
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userName)
        {
            return await GetAsync<IEnumerable<OrderDto>>($"{Address}/user/{userName}");
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            return await GetAsync<OrderDto>($"{Address}/{id}");
        }

        public async Task<OrderDto> CreateOrderAsync(string userName, CreateOrderModel createOrderModel)
        {
            var response = await PostAsync($"{Address}/{userName}", createOrderModel);
            return await response.Content.ReadAsAsync<OrderDto>();
        }

        public async Task<IEnumerable<ProductDto>> GetUsedProductsAsync()
        {
            return await GetAsync<IEnumerable<ProductDto>>($"{Address}/usedproducts");
        }
    }
}
