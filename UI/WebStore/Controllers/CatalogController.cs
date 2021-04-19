using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Domain.ViewModels;
using WebStore.Services.Mapping;

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
                    .ToView()
            });
        }

        public IActionResult Details(int Id)
        {
            var product = _productData.GetProductById(Id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product.ToView());
        }
    }
}
