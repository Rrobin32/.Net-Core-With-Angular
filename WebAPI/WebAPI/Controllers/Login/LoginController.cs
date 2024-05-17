using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        [HttpGet]
        public IActionResult Login([FromQuery]  string userName, [FromQuery] string password)
        {
            return Ok();
        }
    }
}
