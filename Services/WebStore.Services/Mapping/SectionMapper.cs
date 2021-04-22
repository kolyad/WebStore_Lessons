using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Dto;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class SectionMapper
    {
        public static SectionDto ToDto(this Section section) => section is null
            ? null
            : new SectionDto
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                ParentId = section.ParentId,
                ProductsCount = section.Products.Count()
    };        
        
        public static Section FromDto(this SectionDto section) => section is null
            ? null
            : new Section
            {
                Id = section.Id,
                Name = section.Name,
                Order = section.Order,
                ParentId = section.ParentId
            };

        public static IEnumerable<SectionDto> ToDto(this IEnumerable<Section> sections) => sections.Select(ToDto);

        public static IEnumerable<Section> FromDto(this IEnumerable<SectionDto> sections) => sections.Select(FromDto);
    }
}
