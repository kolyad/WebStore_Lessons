using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Areas.Admin.ViewModels;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;
        private readonly IOrderService _orderService;

        public ProductsController(IProductData productData, IOrderService orderService)
        {
            _productData = productData;
            _orderService = orderService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var usedProducts = await _orderService.GetUsedProductsAsync();

            return View(_productData.GetProducts().Select(s => new ProductEditViewModel 
            { 
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                SectionId = s.SectionId,
                SectionName = s.Section?.Name,
                BrandId = s.BrandId,
                BrandName = s.Brand?.Name,
                ImageUrl = s.ImageUrl,
                Price = s.Price,
                CanDelete = !usedProducts.Contains(s)
            }));
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

        [HttpPost]        
        public IActionResult Edit(Product model)
        {
            _ = model ?? throw new ArgumentNullException();

            if (model.Id > 0)
            {
                _productData.Update(model);
            }
            else
            {
                return NotFound();
            }
            
            return RedirectToAction("Index");
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

        [HttpPost]        
        public IActionResult DeleteConfirmed(int Id)
        {
            _productData.Delete(Id);
            return RedirectToAction("Index");
        }

    }
}
