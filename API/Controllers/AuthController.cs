using API.DTOs.loginDTOs;
using API.DTOs.RoleDTOs;
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
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService;

        public AuthController(IMapper mapper, IUserRepository userRepository, IContactRepository contactRepository, ITokenService tokenService, UserManager<User> userManager, RoleManager<Role> roleManager, IEmailService emailService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _contactRepository = contactRepository;
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> CreateAccount(UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO.Password != userRequestDTO.PasswordRepeted)
                return BadRequest("Repeted password is incorrect!");

            var userExist = await _userRepository.GetUserByEmail(userRequestDTO.Email.ToLower());

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

            var result = await _userManager.CreateAsync(newUser, userRequestDTO.Password);
            await _userManager.AddToRoleAsync(newUser, "User");

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var confirmationLink = $"{Url.ActionContext.HttpContext.Request.Scheme}://{Url.ActionContext.HttpContext.Request.Host}/account/confirmEmail?userId={newUser.Id}&token={confirmToken}";

            await _emailService.SendEmailAsync(newUser.Email, confirmationLink);

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

            await _contactRepository.AddContactAsync(contact);

            return Ok(_mapper.Map<UserResponseDTO>(newUser));

        }

        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserdDTO>> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _userRepository.GetUserByEmail(loginRequestDTO.Email.ToLower());

            if (user == null)
                return Unauthorized("Wrong email or password!");

            var result = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!result)
                return Unauthorized("Wrong email or password!");

            LoggedUserdDTO loggedUser = _mapper.Map<LoggedUserdDTO>(user);
            loggedUser.Token = await _tokenService.CreateToken(user);

            return Ok(loggedUser);
        }

        [HttpGet("confirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
                return NotFound("User doesn't exist!");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if(!result.Succeeded)
                return BadRequest("Account activation failed!");

            return Ok("Account is active now!");
        }

    }
}