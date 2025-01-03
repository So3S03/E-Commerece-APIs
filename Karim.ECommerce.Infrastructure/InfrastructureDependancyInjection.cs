﻿using Karim.ECommerce.Domain.Contracts.Infrastructure;
using Karim.ECommerce.Infrastructure.Cache_Services;
using Karim.ECommerce.Infrastructure.Cart_Repository;
using Karim.ECommerce.Infrastructure.WishList_Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Karim.ECommerce.Infrastructure
{
    public static class InfrastructureDependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(IConnectionMultiplexer), serviceProvider =>
            {
                var RedisConnectionString = configuration.GetConnectionString("RedisConnectionString");
                var connectionMultiplexerObj = ConnectionMultiplexer.Connect(RedisConnectionString!);
                return connectionMultiplexerObj;
            });
            services.AddScoped(typeof(ICartRepository), typeof(CartRepository));
            services.AddScoped(typeof(IWishListRepository), typeof(WishListRepository));
            services.AddSingleton(typeof(ICacheService), typeof(CaheService));

            return services;
        }
    }
}
