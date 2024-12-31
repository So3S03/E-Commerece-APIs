namespace Karim.ECommerce.Shared.AppSettingsModels
{
    public class StripeSettings
    {
        public required string SecretKey { get; set; }
        public required string WebHockSecret { get; set; }
    }
}
