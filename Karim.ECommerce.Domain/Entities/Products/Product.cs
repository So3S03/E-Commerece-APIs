using Karim.ECommerce.Domain.Entities.Products._Common;

namespace Karim.ECommerce.Domain.Entities.Products
{
    public class Product : CommonProps<int>
    {
        public double Sold { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Rating { get; set; }
        public List<string>? ImagesCollection { get; set; }

        //Relationships
        public virtual Brand? Brand { get; set; }
        public int? BrandId { get; set; }
        public virtual Category? Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
