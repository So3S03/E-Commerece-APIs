using Karim.ECommerce.Domain.Entities.Products._Common;

namespace Karim.ECommerce.Domain.Entities.Products
{
    public class Category : CommonProps<int>
    {
        public required string CategoryName { get; set; }

        //Relationships
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<CategoryBrand>? Brands { get; set; }
    }
}
