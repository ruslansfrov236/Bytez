using bytez.business.Abstract;
using bytez.data.Context;
using bytez.entity.Entities;
using e=bytez.entity.Entities;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace bytez.business.Concrete
{
        
    public class AppRoleService
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

       
    }
}
