using Gasco.AppLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gasco.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserAppLogic _userAppLogic;

        public UserController(UserAppLogic userAppLogic)
        {
            _userAppLogic = userAppLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userAppLogic.GetAllUsers();

            return response.OkResponse();
        }
    }
}
