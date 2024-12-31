using Karim.ECommerce.APIs.Controllers.Controllers._BaseController;
using Karim.ECommerce.APIs.Controllers.Filters;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Application.Abstraction.Dtos.Products;
using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Params;
using Microsoft.AspNetCore.Mvc;

namespace Karim.ECommerce.APIs.Controllers.Controllers.ProductController
{
    public class ProductController(IServiceManager serviceManager) : ApiControllerBase
    {
        [Cached(timeToLiveInSec: 600)]
        [HttpGet("Products")]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProduct([FromQuery] ProductSpecParams specParams)
        {
            var AllProducts = await serviceManager.ProductServices.GetAllProductsAsync(specParams);
            return Ok(AllProducts);
        }

        [HttpGet("ProductDetails/{Id:int}")]
        public async Task<ActionResult> GetProduct(int Id)
        {
            var Product = await serviceManager.ProductServices.GetProductByIdAsync(Id);
            return Ok(Product);
        }

        [Cached(timeToLiveInSec: 600)]
        [HttpGet("Brands")]
        public async Task<ActionResult> GetAllBrands([FromQuery] BrandSpecParams specParams)
        {
            var AllBrands = await serviceManager.ProductServices.GetAllBrandsAsync(specParams);
            return Ok(AllBrands);
        }

        [HttpGet("BrandDetails/{Id:int}")]
        public async Task<ActionResult> GetBrand(int Id)
        {
            var Brand = await serviceManager.ProductServices.GetBrandByIdAsync(Id);
            return Ok(Brand);
        }

        [Cached(timeToLiveInSec: 600)]
        [HttpGet("Categories")]
        public async Task<ActionResult> GetAllCategories([FromQuery] CategorySpecParams specParams)
        {
            var AllCategories = await serviceManager.ProductServices.GetAllCategoriesAsync(specParams);
            return Ok(AllCategories);
        }

        [HttpGet("CategoryDetails/{Id:int}")]
        public async Task<ActionResult> GetCategory(int Id)
        {
            var Category = await serviceManager.ProductServices.GetCategoryByIdAsync(Id);
            return Ok(Category);
        }
    }
}
