namespace Karim.ECommerce.Shared.Dtos.ThirdPartyDtos
{
    public class EmailDto
    {
        public required string To { get; set; } // Person I'm Gonna Send Email For
        public required string Subject { get; set; } // Email Headline (Subject)
        public required string Body { get; set; } // Email Content
    }
}
