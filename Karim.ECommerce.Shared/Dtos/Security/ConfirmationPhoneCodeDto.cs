using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class ConfirmationPhoneCodeDto
    {
        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public int ConfirmationCode { get; set; }
    }
}
