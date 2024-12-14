using Karim.ECommerce.Shared.Dtos.Carts;

namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface ICartServices
    {
        Task<CartToReturnDto?> GetUserCart(string? cartId);
    }
}
