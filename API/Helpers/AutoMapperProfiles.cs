using API.DTOs.contactDTOs;
using API.DTOs.loginDTOs;
using API.DTOs.ProductDTOs;
using API.DTOs.RoleDTOs;
using API.DTOs.StockDTOs;
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
            CreateMap<UserRequestDTO, User>(); 
            CreateMap<Contact, ContactResponseDTO>();
            CreateMap<ContactRequestDTO, Contact>();
            CreateMap<Role, RoleResponseDTO>();

            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
            
            CreateMap<StockRequest, Stock>();
            CreateMap<Stock, StockResponse>();
        }
    }
}