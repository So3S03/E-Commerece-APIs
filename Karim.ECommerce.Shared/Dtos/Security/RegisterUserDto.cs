using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Diplay Name Is Required!")]
        public required string DisplayName { get; set; }

        [Required(ErrorMessage = "User Name Is Required!")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You Must Provide An Email To Register")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone Number Is Required!")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "You Must Provide An Password To Register")]
        [RegularExpression(@"^(?=.*[^\w\s])(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(.).*\1).{6,}$",
            ErrorMessage = "The Password Must Match This Rules \r\n 1. Accept At Least 1 Non Alphanumeric Chars\r\n 2. Accept At Least 1 Unique Chars \r\n 3. Accept At Least 1 Digit\r\n 4. Accept At Least 1 Upper Case Chars\r\n 5. Accept At Least 1 Lower Case Chars \r\n 6. The Password Minimum Length Must Be 6 ")]
        public required string Password { get; set; }
    }
}
