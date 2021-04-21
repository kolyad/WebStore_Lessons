using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Orders)]
    [ApiController]
    public class OrderApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userName}")]
        public Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userName)
        {
            return _orderService.GetUserOrdersAsync(userName);
        }

        [HttpGet("{id}")]
        public Task<OrderDto> GetOrderByIdAsync(int id)
        {
            return _orderService.GetOrderByIdAsync(id);
        }

        [HttpPost("{userName}")]
        public Task<OrderDto> CreatOrderAsync(string userName, [FromBody] CreateOrderModel createOrderModel)
        {
            return _orderService.CreatOrderAsync(userName, createOrderModel);
        }

        public Task<IEnumerable<ProductDto>> GetUsedProductsAsync()
        {
            return _orderService.GetUsedProductsAsync();
        }
    }
}
