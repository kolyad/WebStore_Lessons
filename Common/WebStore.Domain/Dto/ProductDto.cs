namespace WebStore.Domain.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public BrandDto Brand { get; set; }

        public SectionDto Section { get; set; }
    }
}
