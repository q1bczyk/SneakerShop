using API.DTOs.contactDTOs;
using API.DTOs.OrderDTOs.OrderProductDTOs;

namespace API.DTOs.OrderDTOs
{
    public class OrderResponse
    {
        public DateTime Date { get; set; }
        public ContactResponseDTO Contact { get; set; }
        public List<OrderStockResponse> Stocks { get; set; }
    }
}