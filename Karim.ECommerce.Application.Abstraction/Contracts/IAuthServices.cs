using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Dtos.Security;
using System.Security.Claims;

namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface IAuthServices
    {
        Task<UserDto> LoginAsync(LoginUserDto loginUserDto);
        Task<UserDto> RegisterAsync(RegisterUserDto registerUserDto);
        Task<SuccessDto> EmailConfirmationRequestAsync(EmailConfirmationRequestDto emailConfirmationRequestDto);
        Task<SuccessDto> ConfirmEmailAsync(ConfirmationEmailCodeDto confirmationEmailCodeDto);
        Task<SuccessDto> PhoneConfirmationRequestAsync(PhoneConfirmationRequestDto phoneConfirmationRequestDto);
        Task<SuccessDto> ConfirmPhoneAsync(ConfirmationPhoneCodeDto confirmationPhoneCodeDto);
        Task<SuccessDto> ForgetPasswordByEmailAsync(ForgetPasswordRequestByEmailDto forgetPasswordRequestDto);
        Task<SuccessDto> ForgetPasswordByPhoneAsync(ForgetPasswordRequestByPhoneDto forgetPasswordRequestDto);
        Task<SuccessDto> VerifyCodeByEmailAsync(ResetCodeConfiramtionByEmailDto codeConfirmationDto);
        Task<SuccessDto> VerifyCodeByPhoneAsync(ResetCodeConfirmationByPhoneDto codeConfirmationDto);
        Task<UserDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<UserAddressDto> GetUserAddress(ClaimsPrincipal claimsPrincipal);
        Task<UserAddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, UserAddressDto addressDto);
    }
}
