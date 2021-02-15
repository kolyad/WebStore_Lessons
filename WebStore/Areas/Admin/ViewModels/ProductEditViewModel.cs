namespace WebStore.Areas.Admin.ViewModels
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public int SectionId { get; set; }

        public string SectionName { get; set; }
        
        public int? BrandId { get; set; }

        public string BrandName { get; set; }

        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }

        public bool CanDelete { get; set; }
    }
}
