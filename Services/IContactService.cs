using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IContactService
    {
        Task<ContactViewModel> CreateContactAsync(ContactCreateModel requestModel);
        Task<IEnumerable<ContactViewModel>> GetAllContactAsync();
    }
}
