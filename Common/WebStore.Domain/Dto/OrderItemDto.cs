namespace WebStore.Domain.Dto
{
    public class OrderItemDto
    {
        public int Id { get; set; }        

        public ProductDto Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

}
