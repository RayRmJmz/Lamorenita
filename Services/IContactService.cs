using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IContactService
    {
        Task<ContactViewModel> CreateContactAsync(ContactCreateModel requestModel);
        Task DeleteContact(int contactId);
        Task<IEnumerable<ContactViewModel>> GetAllContactAsync();
        Task<ContactViewModel> GetContactByIdAsync(int contactId);
        Task<ContactViewModel> PutContactAsync(int contactId, ContactCreateModel requestModel);
    }
}
