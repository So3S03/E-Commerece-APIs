namespace Karim.ECommerce.Shared.Dtos.ThirdPartyDtos
{
    public class SmsMessageDto
    {
        public required string PhoneNumber { get; set; }
        public required string Body { get; set; }
    }
}
