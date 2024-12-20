using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class ResetCodeConfirmationByPhoneDto
    {
        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public required int ResetCode { get; set; }
    }
}
