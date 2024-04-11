namespace API.Entities
{
    public class Product : BaseEntity
    {
        public string Producer { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public List<Stock> Stocks { get; set; } = new();
    }
}