using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
        private readonly IDirectionService _directionService;

        public DirectionController(IDirectionService directionService)
        {
            _directionService = directionService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(DirectionCreateModel requestModel)
        {
            return StatusCode(201, await _directionService.CreateDirectionAsync(requestModel));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _directionService.GetAllDirectionAsync());
        }
    }
}
