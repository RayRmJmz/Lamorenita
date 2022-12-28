
using Lamorenita.Models;
using Lamorenita.Services;
using Lamorenita.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamorenita.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ContactViewModel), 201)]
        public async Task<IActionResult> Post(ContactCreateModel requestModel)
        {
            return StatusCode(201, await _contactService.CreateContactAsync(requestModel));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ContactViewModel), 200)]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, await _contactService.GetAllContactAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        [HttpGet("{contactId}")]
        [ProducesResponseType(typeof(ContactViewModel), 200)]
        public async Task<IActionResult> GetUserById(int contactId)
        {
            return StatusCode(200, await _contactService.GetContactByIdAsync(contactId));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="conctactId"></param>
        /// <returns></returns>
        [HttpPut("{conctactId}")]
        [ProducesResponseType(typeof(ContactViewModel), 200)]
        public async Task<IActionResult> PutContactAsync([FromBody] ContactCreateModel requestModel, int conctactId)
        {
            return StatusCode(200, await _contactService.PutContactAsync(conctactId, requestModel));
        }

        [HttpDelete("{conctactId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteContact(int conctactId)
        {
           await _contactService.DeleteContact(conctactId);
            return StatusCode(200);
        }

    }
}
