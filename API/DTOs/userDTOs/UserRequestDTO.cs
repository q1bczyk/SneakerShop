using System.ComponentModel.DataAnnotations;
using API.DTOs.contactDTOs;

namespace API.DTOs.userDTOs
{
    public class UserRequestDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
        [Required, MinLength(8)]
        public string PasswordRepeated { get; set; }
        public ContactRequestDTO Contact { get; set; }
    }
}