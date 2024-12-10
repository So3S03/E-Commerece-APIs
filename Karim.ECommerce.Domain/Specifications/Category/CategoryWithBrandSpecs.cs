using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Shared.Params;
using CategoryEntity = Karim.ECommerce.Domain.Entities.Products.Category;

namespace Karim.ECommerce.Domain.Specifications.Category
{
    public class CategoryWithBrandSpecs : BaseSpecifications<CategoryEntity, int>
    {
        public CategoryWithBrandSpecs(CategorySpecParams specParams)
        {
            IncludesMethod();
            Criteria = C =>
                            (string.IsNullOrEmpty(specParams.Search) || C.NormalizedName.Contains(specParams.Search))
                                &&
                            (!specParams.BrandId.HasValue || C.Brands!.Any(CB => CB.BrandId == specParams.BrandId.Value));

            var SkipedItem = (specParams.PageIndex - 1) * specParams.PageSize;
            ApplyPagination(SkipedItem, specParams.PageSize);
        }

        public CategoryWithBrandSpecs(int Id)
            :base(Id)
        {
            IncludesMethod();
        }

        private protected override void IncludesMethod()
        {
            IncludeStrings.Add($"{nameof(CategoryEntity.Brands)}.{nameof(CategoryBrand.Brand)}");
        }
    }
}
