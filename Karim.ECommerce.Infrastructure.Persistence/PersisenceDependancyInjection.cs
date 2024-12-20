using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Infrastructure.Persistence._SecurityDatabase;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Karim.ECommerce.Infrastructure.Persistence
{
    public static class PersisenceDependancyInjection
    {
        //Main Database
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>((serviceProvider, options) => 
            {
                options
                .UseSqlServer(configuration.GetConnectionString("MainStoreConnectionString"))
                .AddInterceptors(serviceProvider.GetRequiredService<CustomSaveChangesInterceptor>());
            });
            services.AddScoped(typeof(CustomSaveChangesInterceptor));
            services.AddScoped(typeof(IStoreDbInitializer), typeof(StoreDbInitializer));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));


            services.AddDbContext<SecurityDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StoreIdentityConnectionString"));
            });
            services.AddScoped(typeof(ISecurityDbInitializer), typeof(SecurityDbInitializer));
            return services;
        }
        
    }
}
