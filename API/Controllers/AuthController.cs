using API.DTOs.EmailDTOs;
using API.DTOs.loginDTOs;
using API.DTOs.LoginDTOs;
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

        [HttpPost("Register")]
        public async Task<ActionResult<UserResponseDTO>> CreateAccount(UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO.Password != userRequestDTO.PasswordRepeted)
                return BadRequest("Repeted password is incorrect!");

            var userExist = await _userRepository.GetUserByEmail(userRequestDTO.Email.ToLower());

            if (userExist != null)
                return BadRequest("Email is taken!");

            var newUser = _mapper.Map<User>(userRequestDTO);
            newUser.UserName = userRequestDTO.Email.ToLower();

            var result = await _userManager.CreateAsync(newUser, userRequestDTO.Password);
            await _userManager.AddToRoleAsync(newUser, "User");

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            await _emailService.SendConfirmEmailAsync(newUser.Email, newUser.Id,confirmToken);

            var contact = _mapper.Map<Contact>(userRequestDTO.Contact);
            contact.UserId = newUser.Id;

            await _contactRepository.AddContactAsync(contact);

            return Ok(_mapper.Map<UserResponseDTO>(newUser));

        }

        [HttpPost("Login")]
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

        [HttpGet("ConfirmEmail")]
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

        [HttpPost("SendConfirmEmail")]
        public async Task<ActionResult<string>> SendConfirmEmail(EmailRequestDTO emailDTO)
        {
            var user = await _userManager.FindByEmailAsync(emailDTO.Email);

            if(user == null)
                return NotFound("User doesn't exists!");

            if(user.EmailConfirmed)
                return BadRequest("User is already confirmed!");

            string confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendConfirmEmailAsync(user.Email, user.Id, confirmToken);
            
            return Ok("Email with a link has been sent!");
        }

        [HttpPost("PasswordReset")]
        public async Task<ActionResult<string>> PasswordReset(EmailRequestDTO emailDTO){
            var user = await _userManager.FindByEmailAsync(emailDTO.Email);

            if(user == null)
                return NotFound("User doesn't exists!");

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendPasswordResetEmailAsync(user.Email, user.Id, passwordResetToken);

            return Ok("Email with password reset link has been sended");
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult<string>> ChangePassword(string userId, string Token, PasswordResetDTO passwordResetDTO)
        {
            if(passwordResetDTO.NewPassword != passwordResetDTO.RepetedPassword)
                return BadRequest("Repeted password is incorrect!");

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
                return NotFound("User doesn't exist!");

            var result = await _userManager.ResetPasswordAsync(user, Token, passwordResetDTO.NewPassword);

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password has been chagned succesfully!");
        }

        // [HttpPost("AddRoles")]
        // public async Task<ActionResult<string>> AddRoles()
        // {
        //     await _roleManager.CreateAsync(new Role { Name = "User"});
        //     return Ok("Success");
        // }

    }
}