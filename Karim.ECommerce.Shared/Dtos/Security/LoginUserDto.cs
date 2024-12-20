using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "You Must Provide An Email To Login")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "You Must Provide Your Password To Login")]
        public required string Password { get; set; }
    }
}
