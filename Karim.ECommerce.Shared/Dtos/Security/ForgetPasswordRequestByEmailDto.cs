using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class ForgetPasswordRequestByEmailDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
