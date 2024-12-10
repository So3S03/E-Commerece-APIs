using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts;

namespace Karim.ECommerce.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductServices> _productServices;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
        }
        public IProductServices ProductServices => _productServices.Value;
    }
}
