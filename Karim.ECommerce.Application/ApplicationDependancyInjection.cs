using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Application.Mapper;
using Karim.ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Karim.ECommerce.Application
{
    public static class ApplicationDependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            return services;
        }
    }
}
