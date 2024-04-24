using API.DTOs.ProductDTOs;

namespace API.DTOs.OrderDTOs.OrderProductDTOs
{
    public class OrderResponse : ProductBase
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
    }
}