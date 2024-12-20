using Microsoft.AspNetCore.Identity;

namespace Karim.ECommerce.Domain.Entities.Security
{
    public class ApplicationUser : IdentityUser
    {
        public required string DisplayName { get; set; }
        public virtual UserAddress? Address { get; set; }
        public int? EmailConfirmResetCode { get; set; }
        public DateTime? EmailConfirmResetCodeExpiry { get; set; }
        public int? PhoneConfirmResetCode { get; set; }
        public DateTime? PhoneConfirmResetCodeExpiry { get; set; }
    }
}
