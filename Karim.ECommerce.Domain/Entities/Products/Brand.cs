using Karim.ECommerce.Domain.Entities.Products._Common;

namespace Karim.ECommerce.Domain.Entities.Products
{
    public class Brand : CommonProps<int>
    {
        public required string BrandName { get; set; }

        //Relationships
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<CategoryBrand>? Categories { get; set; }
    }
}
