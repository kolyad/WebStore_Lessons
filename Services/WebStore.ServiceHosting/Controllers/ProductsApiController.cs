using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet("sections")]
        public IEnumerable<SectionDto> GetSections()
        {
            return _productData.GetSections();
        }

        [HttpGet("brands")]
        public IEnumerable<BrandDto> GetBrands()
        {
            return _productData.GetBrands();
        }

        [HttpPost]
        public IEnumerable<ProductDto> GetProducts(ProductFilter productFilter = null)
        {
            return _productData.GetProducts(productFilter);
        }

        [HttpGet("{id}")]
        public ProductDto GetProductById(int id)
        {
            return _productData.GetProductById(id);
        }

        [HttpPut]
        public void Update(ProductDto product)
        {
            _productData.Update(product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productData.Delete(id);
        }
    }
}
