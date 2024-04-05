using API.DTOs.contactDTOs;
using API.DTOs.loginDTOs;
using API.DTOs.RoleDTOs;
using API.DTOs.userDTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserResponseDTO>();
            CreateMap<User, LoggedUserdDTO>();
            CreateMap<Contact, ContactResponseDTO>();
            CreateMap<Role, RoleResponseDTO>();
        }
    }
}