using api._Services;
using API.Interfaces;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
       public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}