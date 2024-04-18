using System.ComponentModel.DataAnnotations;

namespace API.DTOs.StockDTOs
{
    public abstract class StockBase
    {
        [Required]
        public string Size { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}