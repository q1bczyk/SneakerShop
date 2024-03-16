using System.Security.Cryptography;
using System.Text;
using API.DTOs.loginDTOs;
using API.DTOs.userDTOs;
using API.Entities;
using API.Extensions;
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
        private readonly ITokenService tokenService;

        public AuthController(IMapper mapper, IUserRepository userRepository, IContactRepository contactRepository, ITokenService tokenService)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.contactRepository = contactRepository;
            this.tokenService = tokenService;
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
                Role = "user",
                IsConfirmed = false,
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

        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserdDTO>> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await userRepository.GetUserByEmail(loginRequestDTO.Email);

            if(user == null)
                return NotFound("User doesn't exist!");

            if(AuthMethodExtension.DecryptPassword(loginRequestDTO.Password, user.PasswordSalt, user.Password) == false)
                return Unauthorized("Wrong password or email!");

            if(!user.IsConfirmed)
                return BadRequest("Account is not active!");

            string token = tokenService.CreateToken(user.Email, user.Role);

            var loggedUserData = new LoggedUserdDTO
            {
                Email = user.Email,
                Role = user.Role,
                Token = token,
            };

            return Ok(loggedUserData);
        }

    }
}