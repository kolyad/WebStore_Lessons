using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product product) => product is null ? null : new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Brand = product.Brand?.Name
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> products) => products.Select(ToView);

        public static ProductDto ToDto(this Product product) => product is null
                 ? null
                 : new ProductDto
                 {
                     Id = product.Id,
                     Name = product.Name,
                     Order = product.Order,
                     Price = product.Price,
                     ImageUrl = product.ImageUrl,
                     Brand = product.Brand.ToDto(),
                     Section = product.Section.ToDto()
                 };

        public static Product FromDto(this ProductDto product) => product is null
            ? null
            : new Product
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                BrandId = product.Brand?.Id,
                Brand = product.Brand.FromDto(),
                SectionId = product.Section.Id,
                Section = product.Section.FromDto()
            };

        public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products) => products.Select(ToDto);

        public static IEnumerable<Product> FromDto(this IEnumerable<ProductDto> products) => products.Select(FromDto);
    }
}
