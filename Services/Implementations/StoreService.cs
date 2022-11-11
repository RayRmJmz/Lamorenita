using AutoMapper;
using Lamorenita.Data_Contexts;
using Lamorenita.Data_Entities;
using Lamorenita.Models;
using Microsoft.EntityFrameworkCore;

namespace Lamorenita.Services.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly LamorenitaDbContext _dbContextService;
        private readonly IMapper _mapperService;

        public StoreService(LamorenitaDbContext dbContextService, IMapper mapperService)
        {
            _dbContextService = dbContextService;
            _mapperService = mapperService;
        }

        public async Task<StoreViewModel> CreateStoreAsync(StoreCreateModel requestModel)
        {
            var store = await _dbContextService.Store.AddAsync(_mapperService.Map<StoreEntity>(requestModel));
            await _dbContextService.SaveChangesAsync();
            return _mapperService.Map<StoreViewModel>(store.Entity);
        }

        public async Task<IEnumerable<StoreViewModel>> GetAllStoreAsync()
        {
            return _mapperService.Map<IEnumerable<StoreViewModel>>(await _dbContextService.Store.ToListAsync());
        }
    }
}
