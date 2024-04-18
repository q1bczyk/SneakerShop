using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Product.PriceDTO
{
    public class PriceRequest
    {
        [Required]
        public int NewPrice { get; set; }
    }
}