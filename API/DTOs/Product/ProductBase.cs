using System.ComponentModel.DataAnnotations;

namespace API.DTOs.ProductDTOs
{
    public abstract class ProductBase
    {
        [Required, MinLength(2)]
        public string Producer { get; set; }
        [Required, MinLength(2)]
        public string Model { get; set; }
        [Required]
        public int Price { get; set; }
        [Required, MinLength(2)]
        public string Color { get; set; }
        [Required, RegularExpression("^(male|female|unisex)$", ErrorMessage = "Gender must be 'male', 'female', or 'unisex'")]
        public string Gender { get; set; }
    }
}