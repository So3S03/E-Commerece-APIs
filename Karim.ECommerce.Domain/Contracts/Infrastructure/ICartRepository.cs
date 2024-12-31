using Karim.ECommerce.Domain.Entities.Carts;

namespace Karim.ECommerce.Domain.Contracts.Infrastructure
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(string id);
        Task<Cart?> UpdateCartAsync(Cart customerCart, TimeSpan CartExpiredTimeSpan);
        Task<bool> ClearCartAsync(string id);
    }
}
