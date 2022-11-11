using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(StoreCreateModel requestModel)
        {
            return StatusCode(201, await _storeService.CreateStoreAsync(requestModel));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _storeService.GetAllStoreAsync());
        }
    }
}
