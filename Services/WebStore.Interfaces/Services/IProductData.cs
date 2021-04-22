using System.Collections.Generic;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDto> GetSections();

        SectionDto GetSectionById(int id);

        IEnumerable<BrandDto> GetBrands();

        BrandDto GetBrandById(int id);

        IEnumerable<ProductDto> GetProducts(ProductFilter productFilter = null);

        ProductDto GetProductById(int id);

        void Update(ProductDto product);

        void Delete(int id);
    }
}
