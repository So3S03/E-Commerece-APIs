using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Application.Abstraction.Dtos.Products;
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Domain.Specifications.Brand;
using Karim.ECommerce.Domain.Specifications.Category;
using Karim.ECommerce.Domain.Specifications.Product;
using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Exceptions;
using Karim.ECommerce.Shared.Params;

namespace Karim.ECommerce.Application.Services
{
    internal class ProductServices(IUnitOfWork unitOfWork, IMapper mapper) : IProductServices
    {
        #region Product
        public async Task<Pagination<ProductToReturnDto>> GetAllProductsAsync(ProductSpecParams specParams)
        {
            var ProductRepo = unitOfWork.GetRepository<Product, int>();
            var ProductSpecs = new ProductWithBrandAndCategorySpecs(specParams);
            var AllProducts = await ProductRepo.GetAllAsyncWithSpecs(ProductSpecs);
            if (AllProducts is null) throw new BadRequestException();
            var CountSpecs = new ProductsWithFilterationCount(specParams.BrandId, specParams.CategoryId, specParams.Search);
            var Count = await ProductRepo.GetItemsCountAsync(CountSpecs);
            var MappedProducts = mapper.Map<IEnumerable<ProductToReturnDto>>(AllProducts);
            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, MappedProducts, Count);
        }

        public async Task<ProductToReturnDto> GetProductByIdAsync(int? Id)
        {
            if (Id is null) throw new BadRequestException();
            var ProductRepo = unitOfWork.GetRepository<Product, int>();
            var ProductSpec = new ProductWithBrandAndCategorySpecs(Id.Value);
            var Product = await ProductRepo.GetAsyncWithSpecs(ProductSpec);
            if (Product is null) throw new NotFoundException(nameof(Domain.Entities.Products.Product), Id.Value);
            var MappedProduct = mapper.Map<ProductToReturnDto>(Product);
            return MappedProduct;
        }
        #endregion

        #region Brand
        public async Task<Pagination<BrandToReturnDto>> GetAllBrandsAsync(BrandSpecParams specParams)
        {
            var BrandRepo = unitOfWork.GetRepository<Brand, int>();
            var BrandsSpecs = new BrandWithCategorySpecs(specParams);
            var AllBrands = await BrandRepo.GetAllAsyncWithSpecs(BrandsSpecs);
            if (AllBrands is null) throw new BadRequestException();
            var CountSpec = new BrandWithFilterationCount(specParams.CategoryId, specParams.Search);
            var Count = await BrandRepo.GetItemsCountAsync(CountSpec);
            var MappedBrands = mapper.Map<IEnumerable<BrandToReturnDto>>(AllBrands);
            return new Pagination<BrandToReturnDto>(specParams.PageIndex, specParams.PageSize, MappedBrands, Count);
        }

        public async Task<BrandToReturnDto> GetBrandByIdAsync(int? Id)
        {
            if (Id is null) throw new BadRequestException();
            var BrandRepo = unitOfWork.GetRepository<Brand, int>();
            var BrandSpecs = new BrandWithCategorySpecs(Id.Value);
            var Brand = await BrandRepo.GetAsyncWithSpecs(BrandSpecs);
            if (Brand is null) throw new NotFoundException(nameof(Domain.Entities.Products.Brand), Id.Value);
            var MappedBrand = mapper.Map<BrandToReturnDto>(Brand);
            return MappedBrand;
        }
        #endregion

        #region Category
        public async Task<Pagination<CategoryToReturnDto>> GetAllCategoriesAsync(CategorySpecParams specParams)
        {
            var CategoryRepo = unitOfWork.GetRepository<Category, int>();
            var CategorySpecs = new CategoryWithBrandSpecs(specParams);
            var AllCategories = await CategoryRepo.GetAllAsyncWithSpecs(CategorySpecs);
            if(AllCategories is null) throw new BadRequestException();
            var CountSpecs = new CategoryWithFilterationCount(specParams.BrandId, specParams.Search);
            var Count = await CategoryRepo.GetItemsCountAsync(CountSpecs);
            var MappedCategories = mapper.Map<IEnumerable<CategoryToReturnDto>>(AllCategories);
            return new Pagination<CategoryToReturnDto>(specParams.PageIndex, specParams.PageSize, MappedCategories, Count);
        }

        public async Task<CategoryToReturnDto> GetCategoryByIdAsync(int? Id)
        {
            if(Id is null) throw new BadRequestException();
            var CategoryRepo = unitOfWork.GetRepository<Category, int>();
            var CategorySpecs = new CategoryWithBrandSpecs(Id.Value);
            var Category = await CategoryRepo.GetAsyncWithSpecs(CategorySpecs);
            if (Category is null) throw new NotFoundException(nameof(Domain.Entities.Products.Category), Id);
            var MappedCategory = mapper.Map<CategoryToReturnDto>(Category);
            return MappedCategory;
        } 
        #endregion
    }
}
