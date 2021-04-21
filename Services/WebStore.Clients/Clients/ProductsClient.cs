using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Clients
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, WebApi.Products) { }

        public IEnumerable<SectionDto> GetSections()
        {
            return Get<IEnumerable<SectionDto>>($"{Address}/sections");
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            return Get<IEnumerable<BrandDto>>($"{Address}/brands");
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter productFilter = null)
        {
            return Post(Address, productFilter ?? new ProductFilter())
                .Content
                .ReadAsAsync<IEnumerable<ProductDto>>()
                .Result;
        }

        public ProductDto GetProductById(int id)
        {
            return Get<ProductDto>($"{Address}/{id}");
        }

        public void Update(ProductDto product)
        {
            Put(Address, product);
        }

        public void Delete(int id)
        {
            Delete($"{Address}/{id}");
        }
    }
}
