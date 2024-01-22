using bytez.data.Abstract;
using bytez.data.Context;
using bytez.data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using bytez.entity.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using bytez.entity.Entities;
using Microsoft.Extensions.Configuration;

namespace bytez.data
{
    public static class ServiceRegistration
    {
        public static void AddDataRegistration(this IServiceCollection service)
        {
            service.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.ConnectionString);
            });

            service.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            })
              .AddEntityFrameworkStores<AppDbContext>();

            service.AddScoped<IProductReadRepository, ProductReadRepository>();
            service.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            service.AddScoped<IProductColorReadRepository, ProductColorReadRepository>();
            service.AddScoped<IProductColorWriteRepository, ProductColorWriteRepository>();
            service.AddScoped<IProductImageReadRepository, ProductImageReadRepository>();
            service.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();
            service.AddScoped<IAboutReadRepository, AboutReadRepository>();
            service.AddScoped<IAboutWriteRepository, AboutWriteRepository>();
            service.AddScoped<IBasketReadRepository, BasketReadRepository>();
            service.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            service.AddScoped<IOrderReadRepository, OrderReadRepository>();
            service.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            service.AddScoped<IBrandModelWriteRepository, BrandModelWriteRepository>();
            service.AddScoped<IBrandModelReadRepository, BrandModelReadRepository>();
            service.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            service.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            service.AddScoped<IHeaderWriteRepository, HeaderWriteRepository>();
            service.AddScoped<IHeaderReadRepository, HeaderReadRepository>();
            service.AddScoped<IConnectionInfoWriteReposItory, ConnectionWriteRepository>();
            service.AddScoped<IConnectionInfoReadReposItory, ConnectionReadRepository>();
        }

        public static async Task AddConfigureRoleAsync(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Define roles
                var roles = new[] { "Admin", "Manager" };

                foreach (var roleName in roles)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));

                    }
                }


            }
        }

        public static async Task AddConfigureUserAdminAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var email = "admin@bytez.com";
                var password = "876.Rac.";
                var identityUser = await userManager.FindByEmailAsync(email);
                if (identityUser == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");


                }

            }
        }
        public static async Task AddConfigureUserManagerAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var email = "manager@bytez.com";
                var password = "876.Raeec.";
                var identityUser = await userManager.FindByEmailAsync(email);
                if (identityUser == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Manager");


                }

            }
        }
    }
}
