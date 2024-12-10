using BrandEntity = Karim.ECommerce.Domain.Entities.Products.Brand;

namespace Karim.ECommerce.Domain.Specifications.Brand
{
    public class BrandWithFilterationCount : BaseSpecifications<BrandEntity, int>
    {
        public BrandWithFilterationCount(int? CategoryId, string? Search) : base()
        {
            Criteria = B =>
                    (string.IsNullOrEmpty(Search) || B.NormalizedName.Contains(Search))
                        &&
                    (!CategoryId.HasValue || B.Categories!.Any(CB => CB.CategoryId == CategoryId.Value));
        }
    }
}
