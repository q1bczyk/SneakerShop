using System.ComponentModel.DataAnnotations;

namespace API.DTOs.OrderDTOs
{
    public class OrderProductRequest
    {
        [Required]
        public string StockId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}