using Karim.ECommerce.Shared.Dtos.Carts;

namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface ICartServices
    {
        Task<CartToReturnDto> GetUserCartAsync(string? cartId);
        Task<CartToReturnDto?> UpdateUserCartAsync(CartToReturnDto? cartToReturnDto);
        Task ClearUserCartAsync(string? cartId);
    }
}
