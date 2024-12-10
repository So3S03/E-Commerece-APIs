using Karim.ECommerce.Application.Abstraction.Dtos.Products._Common;
using Karim.ECommerce.Shared.Dtos.Products;

namespace Karim.ECommerce.Application.Abstraction.Dtos.Products
{
    public class BrandToReturnDto : CommonPropsToReturnDto
    {
        public required string BrandName { get; set; }
        public ICollection<CategoryInBrandDto>? Categories { get; set; }
    }
}
