namespace API.Entities
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public List<Stock> Stocks { get; set; } = new();
        public List<StockOrder> StockOrder { get; set; } = new();
        public string ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}