using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDb _db;

        public SqlProductData(WebStoreDb db)
        {
            _db = db;
        }

        public IEnumerable<Section> GetSections() => _db.Sections.Include(s => s.Products);

        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(s => s.Products);

        public IEnumerable<Product> GetProducts(ProductFilter productFilter = null)
        {
            IQueryable<Product> productQuery = _db.Products
                .Include(i => i.Brand)
                .Include(i => i.Section);

            if (productFilter?.Ids?.Length > 0)
            {
                productQuery = productQuery.Where(x => productFilter.Ids.Contains(x.Id));
            }
            else
            {
                if (productFilter?.SectionId is { } sectionId)
                {
                    productQuery = productQuery.Where(x => x.SectionId == sectionId);
                }

                if (productFilter?.BrandId is { } brandId)
                {
                    productQuery = productQuery.Where(x => x.BrandId == brandId);
                }
            }

            return productQuery;
        }

        public Product GetProductById(int id)
        {
            return _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product is object)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            var entity = _db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (entity is object)
            {
                entity.Name = product.Name;
                entity.Order = product.Order;
                entity.SectionId = product.SectionId;
                entity.BrandId = product.BrandId;                
                entity.ImageUrl = product.ImageUrl;                

                _db.Products.Update(entity);

                _db.SaveChanges();
            }
        }
    }
}
