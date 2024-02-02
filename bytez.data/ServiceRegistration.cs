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
using Microsoft.AspNetCore.Http;

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
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
            
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
            service.AddScoped<IProductBasketReadRepository, ProductBasketReadRepository>();
            service.AddScoped<IProductBasketWriteRepository, ProductBasketWriteRepository>();
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
            service.AddScoped<IBlogWriteRepository, BlogWriteRepository>();
            service.AddScoped<IBlogReadRepository, BlogReadRepository>();

        }
        public static void AddCookieRegistration(this IServiceCollection service)
        {
            service.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = false;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
          

                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".Bytez.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });
        }
    }
}
