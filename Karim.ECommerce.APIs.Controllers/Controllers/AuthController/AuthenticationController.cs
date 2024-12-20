using Karim.ECommerce.APIs.Controllers.Controllers._BaseController;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Dtos.Security;
using Microsoft.AspNetCore.Mvc;

namespace Karim.ECommerce.APIs.Controllers.Controllers.AuthController
{
    public class AuthenticationController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginUserDto loginData)
        {
            var Result = await serviceManager.AuthServices.LoginAsync(loginData);
            return Ok(Result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerData)
        {
            var Result = await serviceManager.AuthServices.RegisterAsync(registerData);
            return Ok(Result);
        }

        [HttpPost("ConfirmEmailCode")]
        public async Task<ActionResult<SuccessDto>> EmailConfirmationRequest(EmailConfirmationRequestDto emailConfirmationRequestDto)
        {
            var Result = await serviceManager.AuthServices.EmailConfirmationRequestAsync(emailConfirmationRequestDto);
            return Ok(Result);
        }

        [HttpPut("ConfirmingEmail")]
        public async Task<ActionResult<SuccessDto>> ConfirmingEmail(ConfirmationEmailCodeDto confirmationEmailCodeDto)
        {
            var Result = await serviceManager.AuthServices.ConfirmEmailAsync(confirmationEmailCodeDto);
            return Ok(Result);
        }

        [HttpPost("ConfirmPhoneCode")]
        public async Task<ActionResult<SuccessDto>> PhoneConfirmationRequest(PhoneConfirmationRequestDto phoneConfirmationRequestDto)
        {
            var Result = await serviceManager.AuthServices.PhoneConfirmationRequestAsync(phoneConfirmationRequestDto);
            return Ok(Result);
        }

        [HttpPut("ConfirmingPhone")]
        public async Task<ActionResult<SuccessDto>> ConfirmingPhone(ConfirmationPhoneCodeDto confirmationPhoneCodeDto)
        {
            var Result = await serviceManager.AuthServices.ConfirmPhoneAsync(confirmationPhoneCodeDto);
            return Ok(Result);
        }

        [HttpPost("ForgetPasswordByEmail")]
        public async Task<ActionResult<SuccessDto>> ForgetPasswordByEmail(ForgetPasswordRequestByEmailDto forgetPasswordRequestDto)
        {
            var Result = await serviceManager.AuthServices.ForgetPasswordByEmailAsync(forgetPasswordRequestDto);
            return Ok(Result);
        }

        [HttpPost("ForgetPasswordByPhone")]
        public async Task<ActionResult<SuccessDto>> ForgetPasswordByPhone(ForgetPasswordRequestByPhoneDto forgetPasswordRequestByPhoneDto)
        {
            var Result = await serviceManager.AuthServices.ForgetPasswordByPhoneAsync(forgetPasswordRequestByPhoneDto);
            return Ok(Result);
        }

        [HttpPost("ResetCodeByEmail")]
        public async Task<ActionResult<SuccessDto>> ResetCodeByEmail(ResetCodeConfiramtionByEmailDto resetCodeConfiramtionDto)
        {
            var Result = await serviceManager.AuthServices.VerifyCodeByEmailAsync(resetCodeConfiramtionDto);
            return Ok(Result);
        }

        [HttpPost("ResetCodeByPhone")]
        public async Task<ActionResult<SuccessDto>> ResetCodeByPhone(ResetCodeConfirmationByPhoneDto resetCodeConfiramtionDto)
        {
            var Result = await serviceManager.AuthServices.VerifyCodeByPhoneAsync(resetCodeConfiramtionDto);
            return Ok(Result);
        }

        [HttpPut("ResetPassword")]
        public async Task<ActionResult<UserDto>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var Result = await serviceManager.AuthServices.ResetPasswordAsync(resetPasswordDto);
            return Ok(Result);
        }
    }
}
