using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IDirectionService
    {
        Task<DirectionViewModel> CreateDirectionAsync(DirectionCreateModel requestModel);
        Task<IEnumerable<DirectionViewModel>> GetAllDirectionAsync();
    }
}
