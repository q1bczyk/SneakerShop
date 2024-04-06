using System.ComponentModel.DataAnnotations;

namespace API.DTOs.EmailDTOs
{
    public class EmailRequestDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}