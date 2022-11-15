using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IUserService
    {
        Task<UserViewModel> CreateUserAsync(UserCreateModel requestModel);
        Task DeleteUser(int userId);
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<UserViewModel> GetUserByIdAsync(int userId);
        Task<UserViewModel> PutUserAsync(int userId, UserUpdateModel requestModel);
        Task<UserViewModel> PutUserPasswordAsync(int userId, UserUpdatePasswordModel requestModel);
    }
}
