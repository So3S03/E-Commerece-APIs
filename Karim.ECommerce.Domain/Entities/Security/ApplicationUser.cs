using Microsoft.AspNetCore.Identity;

namespace Karim.ECommerce.Domain.Entities.Security
{
    public class ApplicationUser : IdentityUser
    {
        public required string DisplayName { get; set; }
        public virtual UserAddress? Address { get; set; }
        public int? ResetCode { get; set; }
        public DateTime? ResetCodeExpiry { get; set; }
    }
}
