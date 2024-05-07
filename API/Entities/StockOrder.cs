namespace API.Entities
{
    public class StockOrder
    {
        public string OrderId { get; set; }
        public string StockId { get; set;}
        public Order Order { get; set; } = null!;
        public Stock Stock { get; set; } = null!;
        public int Quantity { get; set; }
    }
}