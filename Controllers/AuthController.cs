using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auhtService;

        public AuthController(IAuthService auhtService)
        {
            this._auhtService = auhtService;
        }

        /// <summary>
        /// Login usuarios de la App
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthToken), 200)]
        public async Task<IActionResult> login(UserLoginModel model)
        {
            return Ok(await _auhtService.GetAuthTokenAsync(model));
        }



    }
}
