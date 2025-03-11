using Gasco.AppLogic;
using Gasco.Common.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Gasco.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthAppLogic _authAppLogic;

        public AuthController(AuthAppLogic authAppLogic)
        {
            _authAppLogic = authAppLogic;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var response = await _authAppLogic.Login(login);

            return response.OkResponse();
        }
    }
}
