namespace API.Entities
{
    public class Stock : BaseEntity
    {
        public string Size { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}