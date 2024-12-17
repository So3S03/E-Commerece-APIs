using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts;

namespace Karim.ECommerce.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductServices> _productServices;
        private readonly Lazy<ICartServices> _cartServices;
        private readonly Lazy<IAuthServices> _authServices;
        public ServiceManager(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<ICartServices> cartServicesFactory,
            Func<IAuthServices> authServicesFactory)
        {
            _productServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
            _cartServices = new Lazy<ICartServices>(cartServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _authServices = new Lazy<IAuthServices>(authServicesFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public IProductServices ProductServices => _productServices.Value;

        public ICartServices CartServices => _cartServices.Value;

        public IAuthServices AuthServices => _authServices.Value;
    }
}
