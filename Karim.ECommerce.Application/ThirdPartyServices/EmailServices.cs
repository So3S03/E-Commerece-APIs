using Karim.ECommerce.Application.Abstraction.ThirdPartyContracts;
using Karim.ECommerce.Shared.AppSettingsModels;
using Karim.ECommerce.Shared.Dtos.ThirdPartyDtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Karim.ECommerce.Application.ThirdPartyServices
{
    public class EmailServices(IOptions<EmailSettings> emailSettings) : IEmailServices
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;
        public async Task SendEmail(EmailDto emailDto)
        {
            //1. Prepare The Email By MimeMesage Object (Who Will Send - Email Subject)
            var Email = new MimeMessage() 
            {
               Sender = MailboxAddress.Parse(_emailSettings.SenderEmail), //From
               Subject = emailDto.Subject                                 //Subject
            };

            //2. Then Set The Email Reciver
            Email.To.Add(MailboxAddress.Parse(emailDto.To));             //To
            Email.From.Add(new MailboxAddress(_emailSettings.SenderDisplayName, _emailSettings.SenderEmail)); //From

            //3. Then Prepare The Email Body
            var EmailBody = new BodyBuilder();
            EmailBody.TextBody = emailDto.Body;

            //4. But The Email Body In The Email Itself
            Email.Body = EmailBody.ToMessageBody();

            //5. Connect With The SMTP To Send The Email
            using var Smtp = new SmtpClient();
            await Smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);

            //6. Add Auth Settings (Sender Email, Sender Pass)
            await Smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderEmailPassword);

            //7. Then The Final Step Is Sending The Email
            await Smtp.SendAsync(Email);

            //8. Dispose The Connection
            await Smtp.DisconnectAsync(true);
        }
    }
}
