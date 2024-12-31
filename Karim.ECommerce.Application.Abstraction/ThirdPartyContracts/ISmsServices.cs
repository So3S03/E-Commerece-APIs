using Karim.ECommerce.Shared.Dtos.ThirdPartyDtos;
using Twilio.Rest.Api.V2010.Account;

namespace Karim.ECommerce.Application.Abstraction.ThirdPartyContracts
{
    public interface ISmsServices
    {
        Task<MessageResource> SendSms(SmsMessageDto sms);
    }
}
