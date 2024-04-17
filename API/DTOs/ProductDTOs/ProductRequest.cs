using System.ComponentModel.DataAnnotations;
using API.DTOs.StockDTOs;

namespace API.DTOs.ProductDTOs
{
    public class ProductRequest : ProductBase
    {
        [Required]
        public int ProfilePhotoIndex { get; set; }
        [Required]
        public StockRequest[] Stock { get; set; }
        [Required]
        public IFormFile[] Files { get; set; }
    }
}