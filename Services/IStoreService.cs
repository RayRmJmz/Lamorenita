using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IStoreService
    {
        Task<StoreViewModel> CreateStoreAsync(StoreCreateModel requestModel);
        Task<IEnumerable<StoreViewModel>> GetAllStoreAsync();
    }
}
