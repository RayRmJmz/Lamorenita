using AutoMapper;
using Lamorenita.Data_Contexts;
using Lamorenita.Data_Entities;
using Lamorenita.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lamorenita.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapperService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        //private readonly ArensoOrganigramaDbContext _arensoOrganigramaDbContext;
        private readonly LamorenitaDbContext _dbContext;

        public UserService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapperService,
            IConfiguration configuration,
            IPasswordHasher<ApplicationUser> passwordHasher,
            //ArensoOrganigramaDbContext arensoOrganigramaDbContext,
            LamorenitaDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapperService = mapperService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            //_arensoOrganigramaDbContext = arensoOrganigramaDbContext;
            _dbContext = dbContext;
        }

        public async Task<UserFullViewModel> CreateUserAsync(UserRegisterModel userModel)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                ApplicationUser user = _mapperService.Map<ApplicationUser>(userModel);
                var result = await _userManager.CreateAsync(user, _configuration["AppSettings:GenericPassword"]);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(result.Errors.ElementAt(0)?.Description);
                }

                await SetRolesAsync(user, userModel.Roles, false, true);

                user.LockoutEnabled = false;
                await _userManager.UpdateAsync(user);
                transaction.Commit();
                return await GetUserViewModelAsync(user);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new Exception(exception.Message);
            }
        }


        public async Task<UserFullViewModel> CreateInternalUserAsync(UserRegisterModel userModel)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var user = _mapperService.Map<ApplicationUser>(userModel);
                var result = await _userManager.CreateAsync(user, _configuration["AppSettings:GenericPassword"]);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(result.Errors.ElementAt(0)?.Description);
                }
                await SetRolesAsync(user, userModel.Roles, true);
                user.LockoutEnabled = false;
                await _userManager.UpdateAsync(user);
                transaction.Commit();
                return await this.GetUserViewModelAsync(user);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new Exception(exception.Message);
            }
        }


        public async Task<UserFullViewModel> EditUserAsync(string idUser, UserEditModel userEditModel)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var user = await _userManager.FindByIdAsync(idUser);
                if (user == null)
                {
                    throw new NullReferenceException("No se encontro el usuario.");
                }

                _mapperService.Map(userEditModel, user);
                await _userManager.UpdateAsync(user);
                await SetRolesAsync(user, userEditModel.Roles, false, false);

                transaction.Commit();
                return await GetUserViewModelAsync(user);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new Exception(exception.Message);
            }
        }

        public async Task<IEnumerable<UserFullViewModel>> GetUsersAsync()
        {
            List<UserFullViewModel> users = new();
            foreach (var user in await _userManager.Users.Where(u => !u.UserName.Equals("Administrador")).ToListAsync())
            {
                users.Add(await GetUserViewModelAsync(user));
            }
            return users;
        }

        public async Task<UserFullViewModel> ChangePasswordUserAsync(HttpContext httpContext, UserChangePasswordModel userChangePasswordModel)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                throw new NullReferenceException("No se encontro el usuario externo.");
            }
            if (!(await _userManager.IsInRoleAsync(user, "UsuarioExterno")) && !(await _userManager.IsInRoleAsync(user, "Administrador")))
            {
                throw new NullReferenceException("No es usuario externo.");
            }
            var result = await _userManager.ChangePasswordAsync(user, userChangePasswordModel.OldPassword, userChangePasswordModel.NewPassword);
            if (result.Succeeded)
            {
                return _mapperService.Map<UserFullViewModel>(user);
            }
            throw new ArgumentException("No se pudo cambiar la contraseña.");
        }

        public async Task<UserFullViewModel> ResetUserPasswordDefaultAsync(string idUser)
        {
            var user = await _userManager.FindByIdAsync(idUser);
            if (user == null)
            {
                throw new NullReferenceException("No se encontro el usuario externo.");
            }
            user.PasswordHash = _passwordHasher.HashPassword(user, _configuration["AppSettings:GenericPassword"]);
            await _userManager.UpdateAsync(user);
            return _mapperService.Map<UserFullViewModel>(user);
        }

        public async Task<UserFullViewModel> ActiveUserAsync(string idUser, UserActiveModel activeModel)
        {
            var user = await _userManager.FindByIdAsync(idUser);

            if (user == null || !(await _userManager.IsInRoleAsync(user, "UsuarioExterno")))
            {
                throw new NullReferenceException("No se encontro el usuario externo.");
            }
            var result = await _userManager.SetLockoutEnabledAsync(user, !activeModel.Active);
            if (!result.Succeeded)
            {
                throw new ArgumentException("No se pudo Activar o DesActivar el usuario externo.");
            }

            return await GetUserViewModelAsync(user);
        }

        public async Task<UserFullViewModel> GetUserByIdViewModelAsync(string idUser)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(idUser);
            if (user == null)
            {
                throw new NullReferenceException("No se encontro el usuario.");
            }
            return await GetUserViewModelAsync(user);
        }

        public async Task<UserFullViewModel> GetUserViewModelAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userReturn = _mapperService.Map<UserFullViewModel>(user);
            List<RoleUserViewModel> addRoles = new List<RoleUserViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                if (roles.Contains(role.Name))
                {
                    addRoles.Add(_mapperService.Map<RoleUserViewModel>(role));
                }
            }
            userReturn.Roles = addRoles;
            userReturn.Tipo = userReturn.Roles.FirstOrDefault(r => r.Name.Equals("UsuarioExterno")) != null ? "Externo" : "Interno";
            return userReturn;
        }

        public async Task SetRolesAsync(ApplicationUser user, IEnumerable<RoleSelectedModel> roles, bool isInternal, bool isNewUser = false)
        {
            // se eliminan los roles que tiene el usuario
            await RemoveUserRolesAsync(user);
            if (isInternal)
                await _userManager.AddToRoleAsync(user, "UsuarioInterno");
            if (isNewUser && !isInternal)
            {
                await _userManager.AddToRoleAsync(user, "UsuarioExterno");
            }
            IList<string> aRoles = roles.Where(r => r.Selected).Select(r => r.Name).ToList<string>();
            var result = await _userManager.AddToRolesAsync(user, aRoles);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.ElementAt(0)?.Description);
            }
        }

        public async Task RemoveUserRolesAsync(ApplicationUser user)
        {
            IList<string> roles = (await _userManager.GetRolesAsync(user)).Where(r => !r.Contains("UsuarioExterno") && !r.Contains("UsuarioInterno")).ToList<string>();
            if (roles.Count > 0)
            {
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(result.Errors.ElementAt(0)?.Description);
                }
            }

        }


        public async Task<IEnumerable<UserFullViewModel>> GetUsersInRoleAsync(string role)
        {
            List<UserFullViewModel> users = new();
            foreach (var user in await _userManager.GetUsersInRoleAsync(role))
            {
                users.Add(await GetUserViewModelAsync(user));
            }
            return users;
        }

        public async Task<bool> EsUsuarioEnRolAsync(string usuarioId, string role)
        {
            var user = await _dbContext.Users.FindAsync(usuarioId);
            if (user == null) throw new NullReferenceException("Usuario no encontrado, en el proceso de EsUsuarioEnRol");
            var esUsuarioEnRol = await _userManager.IsInRoleAsync(user, role);
            return esUsuarioEnRol;
        }
    }
}

