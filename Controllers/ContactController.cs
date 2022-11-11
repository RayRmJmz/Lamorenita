using Lamorenita.Models;
using Lamorenita.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContactCreateModel requestModel)
        {
            return StatusCode(201, await _contactService.CreateContactAsync(requestModel));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _contactService.GetAllContactAsync());
        }
    }
}
