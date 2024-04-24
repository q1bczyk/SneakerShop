using System.ComponentModel.DataAnnotations;

namespace API.DTOs.OrderDTOs
{
    public class OrderRequest
    {
        [Required]
        public string ContactId { get; set; }
        [Required]
        public OrderProductRequest[] Products { get; set; }
    }
}