namespace API.Entities
{
    public class Product : BaseEntity
    {
        public string Producer { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }
        public List<Stock> Stocks { get; set; } = new();
        public List<Photo> Photos { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
    }
}