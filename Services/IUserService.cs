using Lamorenita.Data_Entities;
using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IUserService
    {
        Task<UserFullViewModel> CreateUserAsync(UserRegisterModel userModel);
        Task<UserFullViewModel> CreateInternalUserAsync(UserRegisterModel userModel);
        Task<UserFullViewModel> EditUserAsync(string idUser, UserEditModel userEditModel);
        Task<UserFullViewModel> ChangePasswordUserAsync(HttpContext httpContext, UserChangePasswordModel userChangePasswordModel);
        Task<UserFullViewModel> ResetUserPasswordDefaultAsync(string idUser);
        Task<IEnumerable<UserFullViewModel>> GetUsersAsync();
        Task<UserFullViewModel> ActiveUserAsync(string idUser, UserActiveModel activeModel);
        Task<UserFullViewModel> GetUserViewModelAsync(ApplicationUser user);
        Task<UserFullViewModel> GetUserByIdViewModelAsync(string idUser);
        Task SetRolesAsync(ApplicationUser user, IEnumerable<RoleSelectedModel> roles, bool isInternal, bool isNewUser = false);
        Task RemoveUserRolesAsync(ApplicationUser user);
    }
}
