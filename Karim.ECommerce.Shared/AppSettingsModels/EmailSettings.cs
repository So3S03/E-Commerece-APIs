namespace Karim.ECommerce.Shared.AppSettingsModels
{
    public class EmailSettings
    {
        public required string SenderEmail { get; set; }
        public required string SenderDisplayName { get; set; }
        public required string SenderEmailPassword { get; set; }
        public required string Host { get; set; }
        public int Port { get; set; }
    }
}
