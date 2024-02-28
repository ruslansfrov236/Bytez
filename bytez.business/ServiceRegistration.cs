using bytez.business.Abstract;
using bytez.business.Concrete;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace bytez.business
{
    public static class ServiceRegistration
    {

        public static void AddBusinessRegistration(this IServiceCollection service)
        {

            service.AddMediatR(typeof(ServiceRegistration));

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
            service.AddScoped<IBlogService, BlogService>();
            service.AddScoped<IVideoService, VideoService>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<IMessageService, MessageService>();
            service.AddScoped<IContactCallService, ContactCallService>();
            service.AddScoped<IContactWallService, ContactWallService>();
            service.AddScoped<IWishlistService, WishlistService>();
            service.AddScoped<IDeliveryService, DeliveryService>();
            service.AddScoped<ICuponService, CuponService>();
            service.AddScoped<IOrderComponentService, OrderComponentService>();



            service.AddScoped<IAppUserService, AppUserService>();
 




        }
    }
}
