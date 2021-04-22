using System;
using System.Collections.Generic;

namespace WebStore.Domain.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime DateOrder { get; set; }

        public IEnumerable<OrderItemDto> Items { get; set; }
    }
}
