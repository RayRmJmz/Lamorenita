using Lamorenita.Migrations;
using Lamorenita.Models;
using Lamorenita.Services;
using Lamorenita.Services.Implementations;
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

        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetUserById(int contactId)
        {
            return StatusCode(200, await _contactService.GetContactByIdAsync(contactId));
        }


        [HttpPut("{conctactId}")]
        public async Task<IActionResult> PutContactAsync([FromBody] ContactCreateModel requestModel, int conctactId)
        {
            return StatusCode(200, await _contactService.PutContactAsync(conctactId, requestModel));
        }

        [HttpDelete("{conctactId}")]
        public async Task<IActionResult> DeleteContact(int conctactId)
        {
           await _contactService.DeleteContact(conctactId);
            return StatusCode(200);
        }

    }
}
