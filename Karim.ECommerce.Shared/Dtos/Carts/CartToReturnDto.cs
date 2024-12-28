using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Carts
{
    public class CartToReturnDto
    {
        [Required]
        public required string CartId { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

    }
}
