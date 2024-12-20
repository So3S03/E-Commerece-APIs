using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Application.Abstraction.ThirdPartyContracts;
using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Shared.AppSettingsModels;
using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Dtos.Security;
using Karim.ECommerce.Shared.Dtos.ThirdPartyDtos;
using Karim.ECommerce.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Karim.ECommerce.Application.Services
{
    internal class AuthServices(
        UserManager<ApplicationUser> userManager,
        IEmailServices emailServices,
        ISmsServices smsServices,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings) : IAuthServices
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<UserDto> LoginAsync(LoginUserDto loginUserDto)
        {
            var User = await userManager.FindByEmailAsync(loginUserDto.Email);
            if (User is null) throw new BadRequestException("Invalid Login");
            var result = await signInManager.CheckPasswordSignInAsync(User, loginUserDto.Password, true);
            if (result.IsLockedOut) throw new BadRequestException("Your Account Is Locked Please Try Again Later");
            if (!result.Succeeded) throw new BadRequestException("Invalid Login");
            var MappedUser = new UserDto()
            {
                UserId = User.Id,
                DisplayName = User.DisplayName,
                Email = User.Email!,
                Token = await GenerateJwtToken(User)
            };
            return MappedUser;
        }

        public async Task<UserDto> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var RegisteredUserByPhone = await userManager.Users.Where(U => U.PhoneNumber == registerUserDto.PhoneNumber).FirstOrDefaultAsync();
            if (RegisteredUserByPhone is not null) throw new BadRequestException("The Provided Phone Number Is Exist, Try Regester With Different Phone Number");
            var User = new ApplicationUser()
            {
                DisplayName = registerUserDto.DisplayName,
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
                PhoneNumber = registerUserDto.PhoneNumber
                
            };
            var result = await userManager.CreateAsync(User, registerUserDto.Password);
            if (!result.Succeeded) throw new ValidationException("Some Data Is Not Valid") { Errors = result.Errors.Select(E => E.Description)};
            var MappedUser = new UserDto()
            {
                UserId = User.Id,
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await GenerateJwtToken(User)
            }; ;
            return MappedUser;
        }

        public async Task<SuccessDto> EmailConfirmationRequestAsync(EmailConfirmationRequestDto emailConfirmationRequestDto)
        {
            var User = await userManager.FindByEmailAsync(emailConfirmationRequestDto.Email);
            if (User is null) throw new BadRequestException("The Provided Email Is Invalid");
            var ConfirmationCode = new Random().Next(100_000, 999_999);
            var ConfirmationCodeExpiryDate = DateTime.UtcNow.AddMinutes(15);
            User.EmailConfirmResetCode = ConfirmationCode;
            User.EmailConfirmResetCodeExpiry = ConfirmationCodeExpiryDate;
            var Result = await userManager.UpdateAsync(User);
            if (!Result.Succeeded) throw new BadRequestException("Something Went Wrong While Sending The Confirmation Code");
            var Email = new EmailDto()
            {
                To = emailConfirmationRequestDto.Email,
                Subject = "Confirm ECommerce Account",
                Body = $"We Have Recived Your Request For Confirming Your Account, \nYour Confirmation Code Is ==> [ {ConfirmationCode} ] <== \nNote: This Code Will Be Expired After 15 Minutes!"
            };
            await emailServices.SendEmail(Email);
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "We Have Sent To You The Confirmation Code"
            };
            return SuccessObj;
        }

        public async Task<SuccessDto> ConfirmEmailAsync(ConfirmationEmailCodeDto confirmationEmailCodeDto)
        {
            var User = await userManager.FindByEmailAsync(confirmationEmailCodeDto.Email);
            if (User is null) throw new BadRequestException("The Provided Email Is Invalid");
            if (User.EmailConfirmResetCode != confirmationEmailCodeDto.ConfirmationCode) throw new BadRequestException("The Provided Code Is Invalid");
            if (User.EmailConfirmResetCodeExpiry < DateTime.UtcNow) throw new BadRequestException("The Provided Code Is Expired");
            User.EmailConfirmed = true;
            var Result = await userManager.UpdateAsync(User);
            if (!Result.Succeeded) throw new BadRequestException("Something Went Wrong While Confirmming The Account");
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "Email Has Been Confirmed"
            };
            return SuccessObj;
        }

        public async Task<SuccessDto> PhoneConfirmationRequestAsync(PhoneConfirmationRequestDto phoneConfirmationRequestDto)
        {
            var User = await userManager.Users.Where(U => U.PhoneNumber == phoneConfirmationRequestDto.PhoneNumber).FirstOrDefaultAsync();
            if (User is null) throw new BadRequestException("The Provided Phone Number Is Invalid");
            var PhoneConfirmationCode = new Random().Next(100_000, 999_999);
            var PhoneConfirmationCodeExpiry = DateTime.UtcNow.AddMinutes(15);
            User.PhoneConfirmResetCode = PhoneConfirmationCode;
            User.PhoneConfirmResetCodeExpiry = PhoneConfirmationCodeExpiry;
            var Result = await userManager.UpdateAsync(User);
            if (!Result.Succeeded) throw new BadRequestException("Something Went Wrong While Sending Confirmation Code");
            var SmsMsg = new SmsMessageDto()
            {
                PhoneNumber = phoneConfirmationRequestDto.PhoneNumber,
                Body = $"We Have Recived Your Request For Confirming Your Phone Number, \nYour Confirmation Code Is ==> [ {PhoneConfirmationCode} ] <== \nNote: This Code Will Be Expired After 15 Minutes!"
            };
            await smsServices.SendSms(SmsMsg);
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "Confirmation Code Has Been Sent To Your Phone Number"
            };
            return SuccessObj;
        }

        public async Task<SuccessDto> ConfirmPhoneAsync(ConfirmationPhoneCodeDto confirmationPhoneCodeDto)
        {
            var User = await userManager.Users.Where(U => U.PhoneNumber == confirmationPhoneCodeDto.PhoneNumber).FirstOrDefaultAsync();
            if (User is null) throw new BadRequestException("The Provided Phone Number Is Invalid");
            if (confirmationPhoneCodeDto.ConfirmationCode != User.PhoneConfirmResetCode) throw new BadRequestException("The Provided Code Is Invalid");
            if (User.PhoneConfirmResetCodeExpiry < DateTime.UtcNow) throw new BadRequestException("The Provided Confirmation Code Has Been Expired");
            User.PhoneNumberConfirmed = true;
            var Result = await userManager.UpdateAsync(User);
            if (!Result.Succeeded) throw new BadRequestException("Something Went Wrong While Confirming Phone Number");
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "Phone Number Has Been Confirmed"
            };
            return SuccessObj;
        }

        public async Task<SuccessDto> ForgetPasswordByEmailAsync(ForgetPasswordRequestByEmailDto forgetPasswordRequestDto)
        {
            var User = await userManager.FindByEmailAsync(forgetPasswordRequestDto.Email);
            if (User is null) throw new BadRequestException("The Account You Try To Reach Doesn't Exist, Please Enter A Valid Email");
            var ResetCode = new Random().Next(100_000, 999_999);
            var ResetCodeExpiry = DateTime.UtcNow.AddMinutes(15);
            User.EmailConfirmResetCode = ResetCode;
            User.EmailConfirmResetCodeExpiry = ResetCodeExpiry;
            var Result = await userManager.UpdateAsync(User);
            if (!Result.Succeeded) throw new BadRequestException("Something Went Wrong While Sending The Reset Code");
            var Email = new EmailDto()
            {
                To = forgetPasswordRequestDto.Email,
                Subject = "Reset Code For ECommerce Account",
                Body = $"We Have Recived Your Request For Reset Your Account Password, \nYour Reset Code Is ==> [ {ResetCode} ] <== \nNote: This Code Will Be Expired After 15 Minutes!"
            };
            await emailServices.SendEmail(Email);
            SuccessDto SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "We Have Sent You The Reset Code"
            };
            return SuccessObj;
        }

        public async Task<SuccessDto> ForgetPasswordByPhoneAsync(ForgetPasswordRequestByPhoneDto forgetPasswordRequestDto)
        {
            var User = await userManager.Users.Where(U => U.PhoneNumber == forgetPasswordRequestDto.PhoneNumber).FirstOrDefaultAsync();
            if (User is null) throw new BadRequestException("The Provided Phone Is Invalid");
            var RestPhoneCode = new Random().Next(100_000, 999_999);
            var ResetPhoneCodeExpiry = DateTime.UtcNow.AddMinutes(15);
            User.PhoneConfirmResetCode = RestPhoneCode;
            User.PhoneConfirmResetCodeExpiry = ResetPhoneCodeExpiry;
            var Result = await userManager.UpdateAsync(User);
            if (!Result.Succeeded) throw new BadRequestException("Something Went Wrong While Sending Reset Code");
            var SmsMsg = new SmsMessageDto()
            {
                PhoneNumber = forgetPasswordRequestDto.PhoneNumber,
                Body = $"We Have Recived Your Request For Reset Your Account Password, \nYour Reset Code Is ==> [ {RestPhoneCode} ] <== \nNote: This Code Will Be Expired After 15 Minutes!"
            };
            await smsServices.SendSms(SmsMsg);
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "We Have Sent You The Reset Code"
            };
            return SuccessObj;
        }

        public async Task<SuccessDto> VerifyCodeByEmailAsync(ResetCodeConfiramtionByEmailDto codeConfirmationDto)
        {
            var User = await userManager.FindByEmailAsync(codeConfirmationDto.Email);
            if (User is null) throw new BadRequestException("The Provided Email Is Incorrect");
            if (codeConfirmationDto.ResetCode != User.EmailConfirmResetCode) throw new BadRequestException("The Code You Have Provided Is Not Valid");
            if (User.EmailConfirmResetCodeExpiry < DateTime.UtcNow) throw new BadRequestException("The Code You Have Provided Is Expired");
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "Reset Code Is Verified, Please Proceed To Change Your Password"
            };
            return SuccessObj;
        }

        public async Task<SuccessDto> VerifyCodeByPhoneAsync(ResetCodeConfirmationByPhoneDto codeConfirmationDto)
        {
            var User = await userManager.Users.Where(U => U.PhoneNumber == codeConfirmationDto.PhoneNumber).FirstOrDefaultAsync();
            if (User is null) throw new BadRequestException("The Provided Phone Number is Invalid");
            if (codeConfirmationDto.ResetCode != User.PhoneConfirmResetCode) throw new BadRequestException("The Provided Code Is Invalid");
            if (User.PhoneConfirmResetCodeExpiry < DateTime.UtcNow) throw new BadRequestException("The Provided Code Has Been Expired");
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "Reset Code Is Verified, Please Proceed To Change Your Password"
            };
            return SuccessObj;
        }

        public async Task<UserDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var User = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (User is null) throw new BadRequestException("The Email Is Invalid");
            var RemovedPassword = await userManager.RemovePasswordAsync(User);
            if (!RemovedPassword.Succeeded) throw new BadRequestException("Something Went Wrong While Reseting Your Password");
            var EnterNewPassword = await userManager.AddPasswordAsync(User, resetPasswordDto.NewPassword);
            if (!EnterNewPassword.Succeeded) throw new BadRequestException("Something Went Wrong While Reseting Your Password");
            var MappedUser = new UserDto()
            {
                UserId = User.Id,
                DisplayName = User.DisplayName,
                Email = User.Email!,
                Token = await GenerateJwtToken(User)
            };
            return MappedUser;
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            //Adding All Claims In One List
            //1.Get All Claims Exist In Database For This User
            var userClaims = await userManager.GetClaimsAsync(user);
            //2.Create List Of The Private Claims I Want To Add Then Unin It With The Claims That Exist In Database,
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, user.Id),           // UserId
                new Claim(ClaimTypes.Email, user.Email!),            // UserEmail
                new Claim(ClaimTypes.GivenName, user.DisplayName),   // User DisplayName
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!) // User PhoneNumber
            }.Union(userClaims).ToList();
            //3.Get User Roles Then Add it In The Private Calims List
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles) 
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            //Start Generating The Security Key And Token Object
            //1.Create Security Key (You Can Provid A Strong Key Throw This Website => https://jwt-keys.21no.de/)
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SymmetricSecurityKey));
            //2.Create Token Object
            var TokenObj = new JwtSecurityToken(
                issuer: _jwtSettings.Issure,
                audience: _jwtSettings.Audience,
                claims: authClaims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiresInHours),
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

            //Create Object From [JwtSecurityTokenHandler] To Use [WriteToken] Method By Give It Token Object As Parameter
            return new JwtSecurityTokenHandler().WriteToken(TokenObj);
        }
    }
}
