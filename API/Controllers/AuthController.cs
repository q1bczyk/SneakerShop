using System.Security.Cryptography;
using System.Text;
using API.DTOs.userDTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API._Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IContactRepository contactRepository;

        public AuthController(IMapper mapper, IUserRepository userRepository, IContactRepository contactRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.contactRepository = contactRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> CreateAccount(UserRequestDTO userRequestDTO)
        {
            if(userRequestDTO.Password != userRequestDTO.PasswordRepeted)
                return BadRequest("Repeted password is incorrect!");

            var userExist = await userRepository.GetUserByEmail(userRequestDTO.Email);

            if(userExist != null)
                return BadRequest("Email is taken!");

            using var hmac = new HMACSHA512();

            var newUser = new User
            {
                Email = userRequestDTO.Email,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRequestDTO.Password)),
                PasswordSalt = hmac.Key,
                Role = "user"
            };

            await userRepository.AddUserAsync(newUser);

            var contact = new Contact
            {
                Name = userRequestDTO.Contact.Name,
                Lastname = userRequestDTO.Contact.Lastname,
                PhoneNumber = userRequestDTO.Contact.PhoneNumber,
                Street = userRequestDTO.Contact.Street,
                StreetNumber = userRequestDTO.Contact.StreetNumber,
                PostalCode = userRequestDTO.Contact.PostalCode,
                UserId = newUser.Id,
            };

            await contactRepository.AddContactAsync(contact);

            return Ok(mapper.Map<UserResponseDTO>(newUser));

        }
    }
}