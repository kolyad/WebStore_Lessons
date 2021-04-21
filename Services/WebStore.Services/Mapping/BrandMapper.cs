using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class BrandMapper
    {
        public static BrandDto ToDto(this Brand brand) => brand is null
            ? null
            : new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order
            };        
        
        public static Brand FromDto(this BrandDto brand) => brand is null
            ? null
            : new Brand
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order
            };

        public static IEnumerable<BrandDto> ToDto(this IEnumerable<Brand> brands) => brands.Select(ToDto);

        public static IEnumerable<Brand> FromDto(this IEnumerable<BrandDto> brands) => brands.Select(FromDto);
    }
}
