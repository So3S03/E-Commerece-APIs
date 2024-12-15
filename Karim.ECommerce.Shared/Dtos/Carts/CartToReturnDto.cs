using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Carts
{
    public class CartToReturnDto
    {
        [Required]
        public required string CartId { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
