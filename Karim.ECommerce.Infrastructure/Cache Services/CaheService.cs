using Karim.ECommerce.Domain.Contracts.Infrastructure;
using StackExchange.Redis;
using System.Text.Json;

namespace Karim.ECommerce.Infrastructure.Cache_Services
{
    public class CaheService(IConnectionMultiplexer redis) : ICacheService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task CacheTheResponseAsync(string cachingKey, object cachingValue, TimeSpan livedCahing)
        {
            if (cachingValue is null) return;
            var SerializedOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var SerializedResponse = JsonSerializer.Serialize(cachingValue, SerializedOptions);
            await _database.StringSetAsync(cachingKey, SerializedResponse, livedCahing);
        }

        public async Task<string?> GetCahedResponseAsync(string cahingKey)
        {
            var Response = await _database.StringGetAsync(cahingKey);
            if(Response.IsNull) return null;
            return Response;
        }
    }
}
