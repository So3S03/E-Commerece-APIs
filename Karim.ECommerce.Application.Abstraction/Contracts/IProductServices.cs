using Karim.ECommerce.Application.Abstraction.Dtos.Products;
using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Params;

namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface IProductServices
    {
        Task<Pagination<ProductToReturnDto>> GetAllProductsAsync(ProductSpecParams specParams);
        Task<ProductToReturnDto> GetProductByIdAsync(int? Id);
        Task<Pagination<BrandToReturnDto>> GetAllBrandsAsync(BrandSpecParams specParams);
        Task<BrandToReturnDto> GetBrandByIdAsync(int? Id);
        Task<Pagination<CategoryToReturnDto>> GetAllCategoriesAsync(CategorySpecParams specParams);
        Task<CategoryToReturnDto> GetCategoryByIdAsync(int? Id);
    }
}
