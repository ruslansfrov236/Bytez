using bytez.business.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;
using e=bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace bytez.business.Concrete
{

    public class AppRoleService : IAppRoleService
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IApplicationBuilder _applicationBuilder;

        public AppRoleService(AppDbContext context , RoleManager<AppRole> roleManager , IApplicationBuilder applicationBuilder)
        {
            _context = context;
            _roleManager= roleManager;
            _applicationBuilder= applicationBuilder;
        }

        public async Task GetAppUpdateROLE( RoleModel roleModel ,string id)
        {
            
            var role = await _context.UserRoles
     .FirstOrDefaultAsync(o => o.RoleId.ToString() == id);

            if (role!=null)
            {
                AppUser user = new() { 
               NormalizedUserName= roleModel.ToString()
                };

                await _context.SaveChangesAsync();

            }

        }

        public async Task<List<AppUser>> GetAppUserROLE()
        {
            var appRole = await _context.Users.Select(r=> new AppUser() { NameSurname =r.NameSurname , Email= r.Email , NormalizedUserName = r.NormalizedUserName}).ToListAsync();

            return appRole;
        }
    }
}
