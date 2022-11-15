using AutoMapper;
using Lamorenita.Data_Contexts;
using Lamorenita.Data_Entities;
using Lamorenita.Models;
using Microsoft.EntityFrameworkCore;

namespace Lamorenita.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly LamorenitaDbContext _dbContextService;
        private readonly IMapper _mapperService;

        public UserService(LamorenitaDbContext dbContextService, IMapper mapperService)
        {
            _dbContextService = dbContextService;
            _mapperService = mapperService;
        }

        public async Task<UserViewModel> CreateUserAsync(UserCreateModel requestModel)
        {
            var user = await _dbContextService.User.AddAsync(_mapperService.Map<UserEntity>(requestModel));
            await _dbContextService.SaveChangesAsync();
            return _mapperService.Map<UserViewModel>(user.Entity);

        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return _mapperService.Map<IEnumerable<UserViewModel>>(await _dbContextService.User.ToListAsync());
        }

        public async Task<UserViewModel> GetUserByIdAsync(int userId)
        {
            UserEntity user = await _dbContextService.User.FindAsync(userId);
            return _mapperService.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> PutUserAsync(int userId, UserUpdateModel requestModel)
        {
            UserEntity user = await _dbContextService.User.FindAsync(userId);

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            _mapperService.Map(requestModel, user);
            await _dbContextService.SaveChangesAsync();
            return _mapperService.Map<UserViewModel>(user);

        }

        public async Task<UserViewModel> PutUserPasswordAsync(int userId, UserUpdatePasswordModel requestModel)
        {
            UserEntity user = await _dbContextService.User.FindAsync(userId);
            

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            user.Password = requestModel.Password;
            await _dbContextService.SaveChangesAsync();
            return _mapperService.Map<UserViewModel>(user);

        }

        public async Task DeleteUser(int userId)
        {
            UserEntity user = await _dbContextService.User.FindAsync(userId);
           
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
      
            _dbContextService.User.Remove(user);
            await _dbContextService.SaveChangesAsync();

        }
    }
}
