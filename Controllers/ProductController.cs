using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateModel requestModel)
        {
            return StatusCode(201, await _productService.CreateProductAsync(requestModel));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _productService.GetAllProductAsync());
        }
    }
}
