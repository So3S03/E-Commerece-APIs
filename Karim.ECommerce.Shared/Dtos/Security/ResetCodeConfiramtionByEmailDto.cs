using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class ResetCodeConfiramtionByEmailDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required int ResetCode { get; set; }
    }
}
