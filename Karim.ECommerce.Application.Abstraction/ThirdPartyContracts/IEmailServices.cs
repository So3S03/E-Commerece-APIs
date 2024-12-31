using Karim.ECommerce.Shared.Dtos.ThirdPartyDtos;

namespace Karim.ECommerce.Application.Abstraction.ThirdPartyContracts
{
    public interface IEmailServices
    {
        Task SendEmail(EmailDto emailDto);
    }
}
