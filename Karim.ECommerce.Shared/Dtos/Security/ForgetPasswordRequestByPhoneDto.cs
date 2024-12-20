using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class ForgetPasswordRequestByPhoneDto
    {
        [Required]
        public required string PhoneNumber { get; set; }
    }
}
