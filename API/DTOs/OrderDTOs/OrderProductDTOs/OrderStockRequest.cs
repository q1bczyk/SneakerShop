using System.ComponentModel.DataAnnotations;

namespace API.DTOs.OrderDTOs
{
    public class OrderStockRequest
    {
        [Required]
        public string StockId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}