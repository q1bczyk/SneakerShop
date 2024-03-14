using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API._Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public AuthController(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> CreateAccount()
        {
            return Ok("Hello World!");
        }
    }
}