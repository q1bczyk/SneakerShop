using System.ComponentModel.DataAnnotations;
using API.Validators;

namespace API.DTOs.StockDTOs
{
    public class StockRequest : StockBase
    {
        [Required, SizeValidator]
        public float Size { get; set; }
    }
}