using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities.Orders;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderItemDto ToDto(this OrderItem item) => item is null
            ? null
            : new OrderItemDto
            {
                Id = item.Id,
                Product = item.Product.ToDto(),
                Price = item.Price,
                Quantity = item.Quantity,
            };

        public static OrderItem FromDto(this OrderItemDto item) => item is null
                   ? null
                   : new OrderItem
                   {
                       Id = item.Id,
                       Product = item.Product.FromDto(),
                       Price = item.Price,
                       Quantity = item.Quantity,
                   };

        public static IEnumerable<OrderItemDto> ToDto(this IEnumerable<OrderItem> items) => items.Select(ToDto);

        public static IEnumerable<OrderItem> FromDto(this IEnumerable<OrderItemDto> items) => items.Select(FromDto);

        public static OrderDto ToDto(this Order order) => order is null
            ? null
            : new OrderDto
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                DateOrder = order.DateOrder,
                Items = order.Items.ToDto()
            };

        public static Order FromDto(this OrderDto order) => order is null
            ? null
            : new Order
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                DateOrder = order.DateOrder,
                Items = order.Items.FromDto().ToList()
            };
    }
}
