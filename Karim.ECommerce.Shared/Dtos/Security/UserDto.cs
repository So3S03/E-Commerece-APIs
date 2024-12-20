namespace Karim.ECommerce.Shared.Dtos.Security
{
    public class UserDto
    {
        public required string UserId { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}
