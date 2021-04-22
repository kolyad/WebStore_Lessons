using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

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
                    ProductsCount = s.ProductsCount
                });

            return View(brand_views);
        }
    }
}
