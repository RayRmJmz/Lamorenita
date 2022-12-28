using Lamorenita.Data_Entities;
using Lamorenita.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Lamorenita.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(IConfiguration config,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._config = config;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<AuthToken> GetAuthTokenAsync(UserLoginModel userModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var appSettingsSection = _config.GetSection("AppSettings");
            var secret = appSettingsSection.GetSection("JwtSecret").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            // Get User
            var user = await _userManager.FindByNameAsync(userModel.UserName);
            if (user == null)
            {
                throw new InvalidCredentialException("El usuario o contraseña es incorrectos.");
            }
            // Ensure the user is allowed to sign in
            if (!await _signInManager.CanSignInAsync(user))
            {
                throw new InvalidCredentialException("El usuario no tiene permitido ingresar.");
            }

            // Ensure the user is not already locked out
            if (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user))
            {
                throw new InvalidCredentialException("El usuario o contraseña es incorrectos.");
            }
            if (user.LockoutEnabled)
            {
                throw new InvalidCredentialException("El usuario no esta activado.");
            }

            // Ensure the password is valid
            if (!await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                if (_userManager.SupportsUserLockout)
                {
                    await _userManager.AccessFailedAsync(user);
                }

                throw new InvalidCredentialException("El usuario o contraseña es incorrectos.");
            }

            // Reset the lockout count
            if (_userManager.SupportsUserLockout)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
            }

            // Look up the user's roles (if any)
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> userRoles = new List<Claim>();
            foreach (var role in roles)
            {
                userRoles.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(10), // Días en que expira el token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            tokenDescriptor.Subject.AddClaims(userRoles);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthToken
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = user.UserName,
                Roles = roles,
                Vigencia = (DateTime)tokenDescriptor.Expires
            };
        }
    }
}
