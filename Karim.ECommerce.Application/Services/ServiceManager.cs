using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts.Infrastructure;
using Karim.ECommerce.Domain.Contracts.Persistence;

namespace Karim.ECommerce.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductServices> _productServices;
        private readonly Lazy<ICartServices> _cartServices;
        private readonly Lazy<IAuthServices> _authServices;
        private readonly Lazy<IWishListServices> _wishListServices;
        private readonly Lazy<IOrderServices> _ordersServices;
        private readonly Lazy<IPaymentService> _paymentService;

        public ServiceManager(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<ICartServices> cartServicesFactory,
            Func<IAuthServices> authServicesFactory,
            Func<IWishListServices> wishListServicesFactory,
            Func<IOrderServices> orderServicesFactory,
            Func<IPaymentService> paymentServicesFactory)
        {
            _productServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
            _cartServices = new Lazy<ICartServices>(cartServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _authServices = new Lazy<IAuthServices>(authServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _wishListServices = new Lazy<IWishListServices>(wishListServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _ordersServices = new Lazy<IOrderServices>(orderServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _paymentService = new Lazy<IPaymentService>(paymentServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public IProductServices ProductServices => _productServices.Value;
        public ICartServices CartServices => _cartServices.Value;
        public IAuthServices AuthServices => _authServices.Value;
        public IWishListServices WishListServices => _wishListServices.Value;
        public IOrderServices OrderServices => _ordersServices.Value;
        public IPaymentService PaymentServices => _paymentService.Value;
    }
}
