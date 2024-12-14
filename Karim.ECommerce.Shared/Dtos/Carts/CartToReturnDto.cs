namespace Karim.ECommerce.Shared.Dtos.Carts
{
    public class CartToReturnDto
    {
        public required string CartId { get; set; }
        public ICollection<CartItemDto>? CartItems { get; set; }
    }
}
