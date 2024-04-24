using API.DTOs.contactDTOs;
using API.DTOs.ProductDTOs;

namespace API.DTOs.OrderDTOs
{
    public class OrderResponse
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public List<ProductResponse> Products { get; set; }
        public ContactResponseDTO Contact { get; set; }
    }
}