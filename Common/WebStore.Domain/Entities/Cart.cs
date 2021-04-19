using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebStore.Domain.Entities
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemsCount => Items?.Sum(x => x.Quantity) ?? 0;
    }
}
