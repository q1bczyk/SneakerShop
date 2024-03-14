using System.ComponentModel.DataAnnotations;
using API.DTOs.contactDTOs;

namespace API.DTOs.userDTOs
{
    public class UserBaseDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}