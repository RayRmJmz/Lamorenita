using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetRolesAsync();
    }
}
