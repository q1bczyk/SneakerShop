namespace API.Entities
{
    public class Stock : BaseEntity
    {
        public float Size { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public List<Order> Orders { get; set; } = new();
        public List<StockOrder> StockOrders { get; set; } = new();
    }
}