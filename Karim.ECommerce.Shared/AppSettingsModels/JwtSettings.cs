namespace Karim.ECommerce.Shared.AppSettingsModels
{
    public class JwtSettings
    {
        public required string Issure { get; set; }
        public required string Audience { get; set; }
        public required double ExpiresInMinutes { get; set; }
        public required string SymmetricSecurityKey { get; set; }
    }
}
