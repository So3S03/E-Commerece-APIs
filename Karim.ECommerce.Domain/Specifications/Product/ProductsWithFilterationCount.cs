using ProductEntity = Karim.ECommerce.Domain.Entities.Products.Product;
namespace Karim.ECommerce.Domain.Specifications.Product
{
    public class ProductsWithFilterationCount : BaseSpecifications<ProductEntity, int>
    {
        public ProductsWithFilterationCount(int? BrandId, int? CategoryId, string? Search) : base()
        {
            Criteria = P => 
                        (string.IsNullOrEmpty(Search) || P.NormalizedName.Contains(Search))
                            &&
                        (!BrandId.HasValue || P.BrandId == BrandId.Value)
                            && 
                        (!CategoryId.HasValue || P.CategoryId == CategoryId.Value );
        }
    }
}
