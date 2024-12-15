using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Carts
{
    public class CartItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public required string ProductName { get; set; }
        public string? PictureUrl { get; set; }

        [Required]
        [Range(.1, double.MaxValue, ErrorMessage = "Price Must Be Greater Than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0.1, int.MaxValue, ErrorMessage = "Quantity Must Be at Least 1 Item")]
        public int Quantity { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
    }
}
