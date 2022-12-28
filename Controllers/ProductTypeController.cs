using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductTypeCreateModel requestModel)
        {
            return StatusCode(201, await _productTypeService.CreateProducTypeAsync(requestModel));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _productTypeService.getAllProductTypeAsync());
        }
    }
}
