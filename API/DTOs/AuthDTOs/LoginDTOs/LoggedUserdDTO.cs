using API.DTOs.RoleDTOs;

namespace API.DTOs.loginDTOs
{
    public class LoggedUserdDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<RoleResponseDTO> Roles { get; set; }
    }
}