namespace WebStore.Domain.Dto
{
    public class BrandDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public int ProductsCount { get; init; }
    }
}
