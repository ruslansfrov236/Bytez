using bytez.business.Abstract;
using bytez.data.Context;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

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

        public async Task ConfigureRoleAsync()
        {
            using (var scope = _applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Define roles
                var roles = new[] { "Admin", "Manager" };

                foreach (var roleName in roles)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                     var role =   await roleManager.CreateAsync(new IdentityRole(roleName));
                

                    }

                }


            }

            await _context.SaveChangesAsync();
           
        }
    }
}
