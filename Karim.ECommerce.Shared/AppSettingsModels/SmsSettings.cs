namespace Karim.ECommerce.Shared.AppSettingsModels
{
    public class SmsSettings
    {
        public required string AccountSID { get; set; }
        public required string AuthToken { get; set; }
        public required string TwilioPhoneNumber { get; set; }
    }
}
