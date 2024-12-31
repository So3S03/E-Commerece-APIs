using Karim.ECommerce.Domain.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Karim.ECommerce.Application.Extensions
{
    internal static class UserManagerExtensions
    {
        public static async Task<ApplicationUser> FindUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var User = await userManager.Users.Where(U => U.Email == Email).Include(U => U.Address).FirstOrDefaultAsync();
            return User!;
        }
    }
}
