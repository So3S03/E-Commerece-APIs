namespace Karim.ECommerce.Shared.Dtos.Orders
{
    public class OrderToReturnDto
    {
        public int OrderId { get; set; }
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; }
        public required OrderAddressDto ShippingAddress { get; set; }
        public virtual ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public string? DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
