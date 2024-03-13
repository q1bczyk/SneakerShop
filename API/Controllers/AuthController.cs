using Microsoft.AspNetCore.Mvc;

namespace API._Controllers
{
    public class AuthController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<string>> CreateAccount()
        {
            return Ok("Hello World!");
        }
    }
}