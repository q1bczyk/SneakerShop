namespace API.Entities
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public List<Product> Products { get; set; } = new();
        public string UserId { get; set; }
        public User User { get; set; }
    }
}