using AutoMapper;
using Lamorenita.Data_Contexts;
using Lamorenita.Data_Entities;
using Lamorenita.Models;
using Microsoft.EntityFrameworkCore;

namespace Lamorenita.Services.Implementations
{
    public class DirectionService : IDirectionService
    {
        private readonly LamorenitaDbContext _dbContextService;
        private readonly IMapper _mapperService;

        public DirectionService(LamorenitaDbContext dbContextService, IMapper mapperService)
        {
            _dbContextService = dbContextService;
            _mapperService = mapperService;
        }

        public async Task<DirectionViewModel> CreateDirectionAsync(DirectionCreateModel requestModel)
        {
            var direction = await _dbContextService.Direction.AddAsync(_mapperService.Map<DirectionEntity>(requestModel));
            await _dbContextService.SaveChangesAsync();
            return _mapperService.Map<DirectionViewModel>(direction.Entity);
        }

        public async Task<IEnumerable<DirectionViewModel>> GetAllDirectionAsync()
        {
            return _mapperService.Map<IEnumerable<DirectionViewModel>>(await _dbContextService.Direction.ToListAsync());
        }
    }
}
