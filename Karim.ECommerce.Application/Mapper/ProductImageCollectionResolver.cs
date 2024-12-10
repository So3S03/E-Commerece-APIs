using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Dtos.Products;
using Karim.ECommerce.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;

namespace Karim.ECommerce.Application.Mapper
{
    internal class ProductImageCollectionResolver(IConfiguration configuration) : IValueResolver<Product, ProductToReturnDto, List<string>?>
    {
        public List<string>? Resolve(Product source, ProductToReturnDto destination, List<string>? destMember, ResolutionContext context)
        {
            if (source.ImagesCollection is not null)
                   return source.ImagesCollection.Select(img => $"{configuration["MainImageBaseUrl"]}{img}").ToList();
            return null;
        }
    }
}
