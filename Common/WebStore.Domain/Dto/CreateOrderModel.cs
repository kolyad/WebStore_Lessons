using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.Dto
{
    public class CreateOrderModel
    {
        public OrderViewModel Order { get; set; }

        public IList<OrderItemDto> Items { get; set; }
    }
}
