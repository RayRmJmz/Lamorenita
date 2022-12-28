using Lamorenita.Models;

namespace Lamorenita.Services
{
    public interface IAuthService
    {
        Task<AuthToken> GetAuthTokenAsync(UserLoginModel userModel);
    }
}
