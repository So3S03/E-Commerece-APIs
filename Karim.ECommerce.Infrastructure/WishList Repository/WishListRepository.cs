using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.WishList;
using StackExchange.Redis;
using System.Text.Json;

namespace Karim.ECommerce.Infrastructure.WishList_Repository
{
    public class WishListRepository(IConnectionMultiplexer redis) : IWishListRepository
    {
        private readonly IDatabase _redis = redis.GetDatabase();
        public async Task<WishList?> CreateUpdateWishListAsync(WishList wishList)
        {
            var SerializedWishList = JsonSerializer.Serialize(wishList);
            var Result = await _redis.StringSetAsync(wishList.WishListId, SerializedWishList);
            return Result ? wishList : null;
        }

        public async Task<WishList?> GetWishListAsync(string WishListId)
        {
            var Result = await _redis.StringGetAsync(WishListId);
            return Result.IsNullOrEmpty ? null : JsonSerializer.Deserialize<WishList>(Result!);
        }

        public async Task<bool> DeleteWishListAsync(string WishListId) => await _redis.KeyDeleteAsync(WishListId);
    }
}
