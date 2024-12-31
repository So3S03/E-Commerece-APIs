using Karim.ECommerce.Shared.Params;
using ProductEntity = Karim.ECommerce.Domain.Entities.Products.Product;
namespace Karim.ECommerce.Domain.Specifications.Product
{
    public class ProductWithBrandAndCategorySpecs : BaseSpecifications<ProductEntity, int>
    {
        public ProductWithBrandAndCategorySpecs(ProductSpecParams specParams) //This is For Getting All {Entity} "Product As An Example"
            :base()
        {
            IncludesMethod(); // For Navigational Property

            AddOrderBy(P => P.ProductName);
            
            if (!string.IsNullOrEmpty(specParams.Sort)) // For Sorting
            {
                switch (specParams.Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    case "mostselling":
                        AddOrderByDesc(P => P.Sold);
                        break;
                    case "newproducts":
                        AddOrderByDesc(P => P.CreatedOn);
                        break;
                    case "name":
                        AddOrderBy(P => P.ProductName);
                        break;
                    default:
                        AddOrderBy(P => P.Id);
                        break;
                }
            }

            Criteria = P =>
                    (string.IsNullOrEmpty(specParams.Search) || P.NormalizedName.Contains(specParams.Search))
                        &&
                    (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value)
                       &&
                    (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value);

            // Want 3rd Page       ==>      "PageIndex = 3"
            // mean i want to skip first 2 Pages     ==>    "SkipedPages = PageIndex - 1      = 2"
            //This for Skipping Pages Now For Skipping Items ==> SkipedItems = (PageIndex - 1) * PageSize
            int SkipedItems = (specParams.PageIndex - 1) * specParams.PageSize; 
            ApplyPagination(SkipedItems, specParams.PageSize);
        }


        public ProductWithBrandAndCategorySpecs()
        {
            IncludesMethod(); 
        }


        public ProductWithBrandAndCategorySpecs(int Id) //This For Getting Specific {Entity}
            :base(Id)
        {
            IncludesMethod();
        }

        private protected override void IncludesMethod()
        {
            IncludeStrings.Add($"{nameof(ProductEntity.Category)}");
            IncludeStrings.Add($"{nameof(ProductEntity.Brand)}");
        }
    }
}
