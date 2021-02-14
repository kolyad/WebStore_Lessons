using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            return View(_productData.GetProducts());
        }

        public IActionResult Edit(int id)
        {
            var product = _productData.GetProductById(id);
            if (product is null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productData.GetProductById(id);
            if (product is null)
            {
                return NotFound();
            }
            return View(product);
        }

    }
}
