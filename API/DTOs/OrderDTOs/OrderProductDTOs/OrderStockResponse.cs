namespace API.DTOs.OrderDTOs.OrderProductDTOs
{
    public class OrderStockResponse 
    {
        public int Quantity { get; set; }
        public OrderProductResponse Product { get; set; }
    }
}