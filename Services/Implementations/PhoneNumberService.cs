using AutoMapper;
using Lamorenita.Data_Contexts;
using Lamorenita.Data_Entities;
using Lamorenita.Models;
using Microsoft.EntityFrameworkCore;

namespace Lamorenita.Services.Implementations
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly LamorenitaDbContext _dbContextService;
        private readonly IMapper _mapperService;

        public PhoneNumberService(LamorenitaDbContext dbContextService, IMapper mapperService)
        {
            _dbContextService = dbContextService;
            _mapperService = mapperService;
        }

        public  async Task<PhoneNumberViewModel> CreatePhoneNumberAsync(PhoneNumberCreateModel requestModel)
        {
            var phoneNumber = await _dbContextService.PhoneNumber.AddAsync(_mapperService.Map<PhoneNumberEntity>(requestModel));
            await _dbContextService.SaveChangesAsync();
            return _mapperService.Map<PhoneNumberViewModel>(phoneNumber.Entity);
        }

        public async Task<IEnumerable<PhoneNumberViewModel>> GetAllPhoneNumberAsync()
        {
            return _mapperService.Map<IEnumerable<PhoneNumberViewModel>>(await _dbContextService.PhoneNumber.ToListAsync());
        }
    }
}
