using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IPhoneNumberService
    {
        Task<PhoneNumberViewModel> CreatePhoneNumberAsync(PhoneNumberCreateModel requestModel);
        Task<IEnumerable<PhoneNumberViewModel>> GetAllPhoneNumberAsync();
    }
}
