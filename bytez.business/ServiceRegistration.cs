using bytez.business.Abstract;
using bytez.business.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace bytez.business
{
    public static class ServiceRegistration
    {

        public static void AddBusinessRegistration(this IServiceCollection service)
        {
       


            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IBrandModelService, BrandModelService>();
            service.AddScoped<IProductColorService, ProductColorService>();
            service.AddScoped<IProductImageService, ProductImageService>();
            service.AddScoped<IAboutService, AboutService>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IBasketService, BasketService>();
            service.AddScoped<IBrandModelService, BrandModelService>();
            service.AddScoped<IHeaderService, HeaderService>();
            service.AddScoped<IConnectionInfoService, ConnectionInfoService>();


            service.AddScoped<IAppUserService, AppUserService>();
       


        }
    }
}
