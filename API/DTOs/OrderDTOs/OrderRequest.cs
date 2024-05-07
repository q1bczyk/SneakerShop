using System.ComponentModel.DataAnnotations;

namespace API.DTOs.OrderDTOs
{
    public class OrderRequest
    {
        [Required]
        public string ContactId { get; set; }
        [Required]
        public OrderStockRequest[] Products { get; set; }
    }
}