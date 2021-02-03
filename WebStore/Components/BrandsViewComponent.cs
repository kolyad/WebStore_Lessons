using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke()
        {
            var brand_views = _productData.GetBrands()
                .OrderBy(x => x.Order)
                .Select(s => new BrandViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    ProductCount = s.Products.Count()
                });

            return View(brand_views);
        }
    }
}
