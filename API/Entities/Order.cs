namespace API.Entities
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<OrderProduct> OrderProducts { get; set; } = new();
        public string ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}