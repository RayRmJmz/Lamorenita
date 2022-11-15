using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserCreateModel requestModel)
        {
            return StatusCode(201, await _userService.CreateUserAsync(requestModel));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _userService.GetAllUsersAsync());
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            return StatusCode(200, await _userService.GetUserByIdAsync(userId));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUserById([FromBody] UserUpdateModel requestModel, int userId)
        {
            return StatusCode(200, await _userService.PutUserAsync(userId, requestModel));
        }

        [HttpPut("updatePassword/{userId}")]
        public async Task<IActionResult> PutUserPasswordById([FromBody] UserUpdatePasswordModel requestModel, int userId)
        {
            return StatusCode(200, await _userService.PutUserPasswordAsync(userId, requestModel));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return StatusCode(200);
        }
    }
}
