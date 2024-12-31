using Karim.ECommerce.Application.Abstraction.ThirdPartyContracts;
using Karim.ECommerce.Shared.AppSettingsModels;
using Karim.ECommerce.Shared.Dtos.ThirdPartyDtos;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Karim.ECommerce.Application.ThirdPartyServices
{
    public class SmsServices(IOptions<SmsSettings> smsSettings) : ISmsServices
    {
        private readonly SmsSettings _smsSettings = smsSettings.Value;

        public async Task<MessageResource> SendSms(SmsMessageDto sms)
        {
            //1. Start Connection With Twilio
            TwilioClient.Init(_smsSettings.AccountSID, _smsSettings.AuthToken);
            //2. Create Sms
            var Result = await MessageResource.CreateAsync(
                body: sms.Body,
                from: new PhoneNumber(_smsSettings.TwilioPhoneNumber),
                to: $"+2{sms.PhoneNumber}"
                );
            //3. Returnning The Message
            return Result;
        }
    }
}
