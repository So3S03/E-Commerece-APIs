using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Orders
{
    public class OrderToCreateDto
    {
        [Required(ErrorMessage = "Cart Id Must Be Provided")]
        public required string CartId { get; set; }

        [Required(ErrorMessage = "A Delivery Method Should Be Settet")]
        public int DeliveryMethod { get; set; }

        [Required(ErrorMessage = "You Should Provide Address To Ship Based On It")]
        public required OrderAddressDto ShippingAddress { get; set; }
    }
}
