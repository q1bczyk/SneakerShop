using api._Services;
using API.Data;
using API.Extensions.OrderExtensions;
using API.Helpers;
using API.Interfaces;
using API.Repository;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
       public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<EmailConfig>(config.GetSection("EmailConfig"));
            services.Configure<BlobStorageConfig>(config.GetSection("BlobStorageConfig"));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IStockOrderRepository, StockOrderRepository>();
            services.AddScoped<PlaceOrderPossibilityExtension>();
            services.AddScoped<PlaceOrderExtension>();

            return services;
        }
    }
}