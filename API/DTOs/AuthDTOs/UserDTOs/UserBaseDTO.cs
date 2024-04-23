using System.ComponentModel.DataAnnotations;

namespace API.DTOs.userDTOs
{
    public abstract class UserBaseDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}