using Karim.ECommerce.Domain.Contracts;

namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductServices ProductServices { get; }
        public ICartServices CartServices { get; }
        public IAuthServices AuthServices { get; }
        public IWishListServices WishListServices { get; }
        public IOrderServices OrderServices { get; }
        public IPaymentService PaymentServices { get; }
    }
}
