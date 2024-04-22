using System.ComponentModel.DataAnnotations;
using API.Validators;

namespace API.DTOs.StockDTOs
{
    public abstract class StockBase
    {
        [Required, SizeValidator]
        public float Size { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}