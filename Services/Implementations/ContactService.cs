using AutoMapper;
using Lamorenita.Data_Contexts;
using Lamorenita.Data_Entities;
using Lamorenita.Models;
using Microsoft.EntityFrameworkCore;

namespace Lamorenita.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly LamorenitaDbContext _dbContextService;
        private readonly IMapper _mapperService;

        public ContactService(LamorenitaDbContext dbContextService, IMapper mapperService)
        {
            _dbContextService = dbContextService;
            _mapperService = mapperService;
        }

        public async Task<ContactViewModel> CreateContactAsync(ContactCreateModel requestModel)
        {
            var contact = await _dbContextService.Contact.AddAsync(_mapperService.Map<ContactEntity>(requestModel));
            await _dbContextService.SaveChangesAsync();
            return _mapperService.Map<ContactViewModel>(contact.Entity);
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllContactAsync()
        {
            return _mapperService.Map<IEnumerable<ContactViewModel>>(await _dbContextService.Contact.ToListAsync());
        }

        public async Task<ContactViewModel> GetContactByIdAsync(int contactId)
        {
            ContactEntity conctact = await _dbContextService.Contact.FindAsync(contactId);
            return _mapperService.Map<ContactViewModel>(conctact);
        }

        public async Task<ContactViewModel> PutContactAsync(int contactId, ContactCreateModel requestModel)
        {
            ContactEntity contact = await _dbContextService.Contact.FindAsync(contactId);

            if (contactId == null)
            {
                throw new ArgumentNullException(nameof(contactId));
            }

            _mapperService.Map(requestModel, contact);
            await _dbContextService.SaveChangesAsync();
            return _mapperService.Map<ContactViewModel>(contact);

        }
        public async Task DeleteContact(int contactId)
        {
            ContactEntity contact = await _dbContextService.Contact.FindAsync(contactId);

            if (contactId == null)
            {
                throw new ArgumentNullException(nameof(contactId));
            }

            _dbContextService.Contact.Remove(contact);
            await _dbContextService.SaveChangesAsync();

        }
    }
}
