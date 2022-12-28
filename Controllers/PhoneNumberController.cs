using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumberController : ControllerBase
    {
        private readonly IPhoneNumberService _phoneNumberService;

        public PhoneNumberController(IPhoneNumberService phoneNumberService )
        {
            _phoneNumberService = phoneNumberService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(PhoneNumberCreateModel requestModel)
        {
            return StatusCode(201, await _phoneNumberService.CreatePhoneNumberAsync(requestModel));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _phoneNumberService.GetAllPhoneNumberAsync());
        }
    }
}
