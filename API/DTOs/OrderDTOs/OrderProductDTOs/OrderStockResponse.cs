namespace API.DTOs.OrderDTOs.OrderProductDTOs
{
    public class OrderStockResponse 
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public OrderProductResponse Product { get; set; }
    }
}