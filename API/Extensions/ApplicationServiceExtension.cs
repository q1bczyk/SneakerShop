namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
       public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen();
            return services;
        }
    }
}