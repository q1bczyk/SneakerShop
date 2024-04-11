using System.ComponentModel.DataAnnotations;

namespace API.DTOs.StockDTOs
{
    public class StockBase
    {
        [Required]
        public string Size { get; set; }
        public int? Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}