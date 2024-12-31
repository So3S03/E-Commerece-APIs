namespace Karim.ECommerce.Domain.Contracts.Infrastructure
{
    public interface ICacheService
    {
        Task CacheTheResponseAsync(string cachingKey, object cachingValue, TimeSpan livedCahing);
        Task<string?> GetCahedResponseAsync(string cahingKey);
    }
}
