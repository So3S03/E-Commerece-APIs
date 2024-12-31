using Karim.ECommerce.Domain.Contracts.Persistence;
using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;

namespace Karim.ECommerce.Infrastructure.Persistence._SecurityDatabase
{
    public class SecurityDbInitializer(SecurityDbContext securityDbContext, UserManager<ApplicationUser> userManager) : DbInitializer(securityDbContext), ISecurityDbInitializer
    {
        public override async Task SeedAsync()
        {
            var User = new ApplicationUser()
            {
                DisplayName = "Karim Fathy",
                UserName = "Karim.Fathy2001",
                Email = "ka2001rimahmed@gmail.com",
                PhoneNumber = "01122339429"
            };
            await userManager.CreateAsync(User, "Karim@2001");
        }
    }
}
