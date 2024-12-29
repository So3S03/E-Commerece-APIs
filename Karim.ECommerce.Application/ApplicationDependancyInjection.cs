using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Application.Abstraction.ThirdPartyContracts;
using Karim.ECommerce.Application.Mapper;
using Karim.ECommerce.Application.Services;
using Karim.ECommerce.Application.ThirdPartyServices;
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Infrastructure.Payment_Services;
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

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient(typeof(IEmailServices), typeof(EmailServices));

            services.Configure<SmsSettings>(configuration.GetSection("SmsSettings"));
            services.AddTransient(typeof(ISmsServices), typeof(SmsServices));

            services.AddScoped(typeof(IWishListServices), typeof(WishListServices));
            services.AddScoped(typeof(Func<IWishListServices>), serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IWishListServices>();
            });

            services.AddScoped(typeof(IOrderServices), typeof(OrderServices));
            services.AddScoped(typeof(Func<IOrderServices>), servicesProvider =>
            {
                return () => servicesProvider.GetRequiredService<IOrderServices>();
            });

            services.AddScoped(typeof(IPaymentService), typeof(PaymentServices));
            services.AddScoped(typeof(Func<IPaymentService>), serviceProvide =>
            {
                return () => serviceProvide.GetRequiredService<IPaymentService>();
            });

            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            return services;
        }
    }
}
