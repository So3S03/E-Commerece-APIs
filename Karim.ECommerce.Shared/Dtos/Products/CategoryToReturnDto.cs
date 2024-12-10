using Karim.ECommerce.Application.Abstraction.Dtos.Products._Common;
using Karim.ECommerce.Shared.Dtos.Products;

namespace Karim.ECommerce.Application.Abstraction.Dtos.Products
{
    public class CategoryToReturnDto : CommonPropsToReturnDto
    {

        public required string CategoryName { get; set; }
        public ICollection<BrandInCategoryDto> Brands { get; set; }

    }
}
