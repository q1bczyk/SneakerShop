using System.ComponentModel.DataAnnotations;
using API.DTOs.StockDTOs;

namespace API.DTOs.ProductDTOs
{
    public class ProductRequest : ProductBase
    {
        [Required]
        public StockRequest[] StockRequest { get; set; }
        [Required]
        public IFormFile []Files { get; set; }
    }
}