using Karim.ECommerce.Shared.Dtos.Carts;

namespace Karim.ECommerce.Domain.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<CartToReturnDto?> CreateUpdatePaymentIntent(string cartId);
        Task UpdateOrderPaymentStatus(string requestBody, string header);
    }
}
