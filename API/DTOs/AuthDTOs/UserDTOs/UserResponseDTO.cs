using API.DTOs.contactDTOs;
using API.DTOs.RoleDTOs;

namespace API.DTOs.userDTOs
{
    public class UserResponseDTO : UserBaseDTO
    {
        public string Id { get; set; }
        public List<ContactResponseDTO> Contacts { get; set; }
        public List<RoleResponseDTO> Roles { get; set; }
    }
}