namespace WebStore.Domain.Dto
{
    public class SectionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public int? ParentId { get; set; }

        public int ProductsCount { get; set; }
    }
}
