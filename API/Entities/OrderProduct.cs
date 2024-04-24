namespace API.Entities
{
    public class OrderProduct
    {
        public string OrderId { get; set; }
        public string ProductId { get; set;}
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}