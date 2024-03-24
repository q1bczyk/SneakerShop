using API.DTOs.loginDTOs;
using API.DTOs.userDTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API._Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IContactRepository contactRepository;
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public AuthController(IMapper mapper, IUserRepository userRepository, IContactRepository contactRepository, ITokenService tokenService, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.contactRepository = contactRepository;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> CreateAccount(UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO.Password != userRequestDTO.PasswordRepeted)
                return BadRequest("Repeted password is incorrect!");

            var userExist = await userRepository.GetUserByEmail(userRequestDTO.Email.ToLower());

            if (userExist != null)
                return BadRequest("Email is taken!");

            var newUser = new User
            {
                Email = userRequestDTO.Email.ToLower(),
                UserName = userRequestDTO.Email.ToLower(),
            };

            // var roleExists = await roleManager.RoleExistsAsync("User");
            // if (!roleExists)
            // {
            //     // Jeśli rola nie istnieje, możesz ją utworzyć
            //     var createRoleResult = await roleManager.CreateAsync(new Role { Name = "User"});
            //     if (!createRoleResult.Succeeded)
            //         return BadRequest(createRoleResult.Errors);
            // }

            var result = await userManager.CreateAsync(newUser, userRequestDTO.Password);
            await userManager.AddToRoleAsync(newUser, "User");

            if (!result.Succeeded)
                return BadRequest(result.Errors);

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
            var user = await userRepository.GetUserByEmail(loginRequestDTO.Email.ToLower());

            if (user == null)
                return Unauthorized("Wrong email or password!");

            var result = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!result)
                return Unauthorized("Wrong email or password!");

            LoggedUserdDTO loggedUser = new LoggedUserdDTO
            {
                Email = user.Email,
                Token = await tokenService.CreateToken(user)
            };

            return Ok(loggedUser);
        }

    }
}