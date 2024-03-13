using Microsoft.AspNetCore.Mvc;

namespace API._Controllers
{
    public class AuthController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<string>> Test()
        {
            return Ok("Hello World!");
        }
    }
}