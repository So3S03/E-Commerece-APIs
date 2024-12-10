using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Dtos.Products._Common;
using Karim.ECommerce.Domain.Entities.Products._Common;
using Microsoft.Extensions.Configuration;

namespace Karim.ECommerce.Application.Mapper
{
    internal class ImageResolver<TSource, TDest>(IConfiguration configuration) : IValueResolver<TSource, TDest, string?>
        where TSource : CommonProps<int>
        where TDest : CommonPropsToReturnDto
    {
        public string? Resolve(TSource source, TDest destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.MainImage))
                return $"{configuration["MainImageBaseUrl"]}{source.MainImage}";
            return string.Empty;
        }
    }
}
