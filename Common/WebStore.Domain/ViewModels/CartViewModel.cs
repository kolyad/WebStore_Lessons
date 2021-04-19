using System.Collections.Generic;
using System.Linq;

namespace WebStore.Domain.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }

        public int ItemsCount => Items?.Sum(x => x.Quantity) ?? 0;

        public decimal TotalPrice => Items?.Sum(x => x.Quantity * x.Product.Price) ?? 0m;
    }
}
