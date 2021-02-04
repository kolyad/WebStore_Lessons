﻿using Microsoft.EntityFrameworkCore;
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
            IQueryable<Product> productQuery = _db.Products;

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
    }
}