using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Application.Mapper;
using Karim.ECommerce.Application.Services;
using Karim.ECommerce.Shared.AppSettingsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Karim.ECommerce.Application
{
    public static class ApplicationDependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings")); //Not Sure

            services.AddScoped(typeof(IProductServices), typeof(ProductServices));

            services.AddScoped(typeof(ICartServices), typeof(CartServices));
            services.AddScoped(typeof(Func<ICartServices>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<ICartServices>();
            });

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddScoped(typeof(IAuthServices), typeof(AuthServices));
            services.AddScoped(typeof(Func<IAuthServices>), serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IAuthServices>();
            });

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            return services;
        }
    }
}
