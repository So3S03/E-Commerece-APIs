using Karim.ECommerce.Shared.Contracts;
using System.Security.Claims;

namespace Karim.ECommerce.APIs.Services
{
    public class LoggedInUserService(IHttpContextAccessor httpContextAccessor) : ILoggedInUserService
    {
        public string UserId => httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.PrimarySid)!;
    }
}
