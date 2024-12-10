using CategoryEntity = Karim.ECommerce.Domain.Entities.Products.Category;

namespace Karim.ECommerce.Domain.Specifications.Category
{
    public class CategoryWithFilterationCount : BaseSpecifications<CategoryEntity, int>
    {
        public CategoryWithFilterationCount(int? BrandId, string? Search) : base()
        {
            Criteria = C =>
                            (string.IsNullOrEmpty(Search) || C.NormalizedName.Contains(Search))
                                &&
                            (!BrandId.HasValue || C.Brands!.Any(CB => CB.BrandId == BrandId.Value));
        }
    }
}
