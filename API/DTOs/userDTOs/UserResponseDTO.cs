using API.DTOs.contactDTOs;

namespace API.DTOs.userDTOs
{
    public class UserResponseDTO : UserBaseDTO
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public List<ContactResponseDTO> Contacts { get; set; }
    }
}