using System.Collections.Generic;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDto> GetSections();

        IEnumerable<BrandDto> GetBrands();

        IEnumerable<ProductDto> GetProducts(ProductFilter productFilter = null);

        ProductDto GetProductById(int id);

        void Update(ProductDto product);

        void Delete(int id);
    }
}
