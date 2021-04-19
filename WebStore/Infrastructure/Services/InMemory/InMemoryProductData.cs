using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Product> GetProducts(ProductFilter productFilter = null)
        {
            var productQuery = TestData.Products;

            if (productFilter?.SectionId is { } sectionId)
            {
                productQuery = productQuery.Where(x => x.SectionId == sectionId);
            }

            if (productFilter?.BrandId is { } brandId)
            {
                productQuery = productQuery.Where(x => x.BrandId == brandId);
            }

            return productQuery;
        }

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(x => x.Id == id);
    }
}
