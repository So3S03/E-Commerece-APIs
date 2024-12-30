namespace Karim.ECommerce.Domain.Entities.Carts
{
    public class Cart
    {
        public required string CartId { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
