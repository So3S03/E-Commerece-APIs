using Karim.ECommerce.Domain.Entities._Base;

namespace Karim.ECommerce.Domain.Entities.Orders
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public required string ShortName { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
        public required string DeliveryTime { get; set; }
    }
}
