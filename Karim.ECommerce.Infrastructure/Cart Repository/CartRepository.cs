using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.Carts;
using StackExchange.Redis;
using System.Text.Json;

namespace Karim.ECommerce.Infrastructure.Cart_Repository
{
    public class CartRepository(IConnectionMultiplexer redis) : ICartRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task<Cart?> GetCartAsync(string id)
        {
            var cart = await _database.StringGetAsync(id);
            return cart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(cart!);
        }

        public async Task<Cart?> UpdateCartAsync(Cart customerCart, TimeSpan CartExpiredTimeSpan) //This is For Creating And Updating The Cart
        {
            var SerializedCart = JsonSerializer.Serialize(customerCart);
            var updated = await _database.StringSetAsync(customerCart.CartId, SerializedCart, CartExpiredTimeSpan);
            return updated ?  customerCart : null;
        }

        public async Task<bool> ClearCartAsync(string id) => await _database.KeyDeleteAsync(id);

    }
}
