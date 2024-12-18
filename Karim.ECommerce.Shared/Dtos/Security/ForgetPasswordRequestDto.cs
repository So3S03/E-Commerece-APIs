using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class ForgetPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
