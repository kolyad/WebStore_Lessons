using System.Collections.Generic;
using System.Linq;
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
            var productQuery = TestData.Products.AsEnumerable();

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

        public void Delete(int id)
        {
            var product = TestData.Products.FirstOrDefault(x => x.Id == id);
            if (product is object)
            {
                TestData.Products.Remove(product);
            }
        }

        public void Update(Product product)
        {
            var entity = TestData.Products.FirstOrDefault(x => x.Id == product.Id);
            if (entity is object)
            {
                entity.Name = product.Name;
                entity.Order = product.Order;
                entity.SectionId = product.SectionId;
                entity.Section = product.Section;
                entity.BrandId = product.BrandId;
                entity.Brand = product.Brand;
                entity.ImageUrl = product.ImageUrl;                
            }
        }
    }
}
