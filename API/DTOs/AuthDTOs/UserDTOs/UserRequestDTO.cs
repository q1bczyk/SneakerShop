using System.ComponentModel.DataAnnotations;
using API.DTOs.contactDTOs;
using API.DTOs.RoleDTOs;

namespace API.DTOs.userDTOs
{
    public class UserRequestDTO : UserBaseDTO
    {
        [Required, MinLength(8)]
        public string Password { get; set; }
        [Required, MinLength(8)]
        public string PasswordRepeted { get; set; }
        [Required]
        public ContactRequestDTO Contact { get; set; }
    }
}