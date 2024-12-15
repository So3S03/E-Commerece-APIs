namespace Karim.ECommerce.Domain.Entities.Carts
{
    public class Cart
    {
        public required string CartId { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
