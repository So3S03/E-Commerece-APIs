using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Shared.Params;
using BrandEntity = Karim.ECommerce.Domain.Entities.Products.Brand;

namespace Karim.ECommerce.Domain.Specifications.Brand
{
    public class BrandWithCategorySpecs : BaseSpecifications<BrandEntity, int>
    {
        public BrandWithCategorySpecs(BrandSpecParams specParams)
        {
            IncludesMethod();
            Criteria = B =>
                            (string.IsNullOrEmpty(specParams.Search) || B.NormalizedName.Contains(specParams.Search))
                                &&
                            (!specParams.CategoryId.HasValue || B.Categories!.Any(CB => CB.CategoryId == specParams.CategoryId.Value));
            var SkipedItems = (specParams.PageIndex - 1) * specParams.PageSize;
            ApplyPagination(SkipedItems, specParams.PageSize);
        }

        public BrandWithCategorySpecs(int Id)
            :base(Id) 
        {
            IncludesMethod();
        }

        private protected override void IncludesMethod()
        {
            IncludeStrings.Add($"{nameof(BrandEntity.Categories)}.{nameof(CategoryBrand.Category)}");
        }
    }
}
