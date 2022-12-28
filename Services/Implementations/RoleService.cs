using AutoMapper;
using Lamorenita.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lamorenita.Services.Implementations
{
    public class RoleService : IRoleService
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync() =>
            _mapper.Map<IEnumerable<RoleViewModel>>(await _roleManager.Roles
                .Where(role => role.Name.ToLower() != "administrador" &&
                !role.Name.Equals("UsuarioInterno") &&
                !role.Name.Equals("UsuarioExterno")).ToListAsync());

    }
}
