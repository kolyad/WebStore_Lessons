using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index(int? sectionId, int? brandId)
        {
            var filter = new ProductFilter
            {
                SectionId = sectionId,
                BrandId = brandId
            };

            var products = _productData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = sectionId,
                BrandId = brandId,
                Products = products
                    .OrderBy(x => x.Order)
                    .Select(s => new ProductViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        ImageUrl = s.ImageUrl,
                        Price = s.Price
                    })
            });
        }
    }
}
