using Karim.ECommerce.Application.Abstraction.Dtos.Products._Common;

namespace Karim.ECommerce.Application.Abstraction.Dtos.Products
{
    public class ProductToReturnDto : CommonPropsToReturnDto
    {
        public double Sold { get; set; }
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Rating { get; set; }
        public List<string>? ImagesCollection { get; set; }

        //Relationships
        public string? Brand { get; set; }
        public int? BrandId { get; set; }
        public string? Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
