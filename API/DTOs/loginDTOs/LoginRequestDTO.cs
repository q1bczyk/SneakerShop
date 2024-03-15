using System.ComponentModel.DataAnnotations;

namespace API.DTOs.loginDTOs
{
    public class LoginRequestDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}