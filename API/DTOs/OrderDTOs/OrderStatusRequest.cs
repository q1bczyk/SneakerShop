using System.ComponentModel.DataAnnotations;
using API.Validators;

namespace API.DTOs.OrderDTOs
{
    public class OrderStatusRequest
    {
        [Required, OrderStatusValidator]
        public string Status { get; set; }
    }
}