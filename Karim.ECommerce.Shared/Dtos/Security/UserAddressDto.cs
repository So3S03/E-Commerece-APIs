using System.ComponentModel.DataAnnotations;

namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class UserAddressDto
    {
        [Required(ErrorMessage = "First Name Must Be Provided")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Must Be Provided")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Street Must Be Provided")]
        public required string Street { get; set; }

        [Required(ErrorMessage = "City Must Be Provided")]
        public required string City { get; set; }

        [Required(ErrorMessage = "Country Must Be Provided")]
        public required string Country { get; set; }
    }
}
