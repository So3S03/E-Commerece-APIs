using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Application.Abstraction.ThirdPartyContracts;
using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Shared.AppSettingsModels;
using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Dtos.Security;
using Karim.ECommerce.Shared.Dtos.ThirdPartyDtos;
using Karim.ECommerce.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
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

        public async Task<SuccessDto> ForgetPasswordByEmailAsync(ForgetPasswordRequestDto forgetPasswordRequestDto)
        {
            var User = await userManager.FindByEmailAsync(forgetPasswordRequestDto.Email);
            if (User is null) throw new BadRequestException("The Account You Try To Reach Doesn't Exist, Please Enter A Valid Email");
            var ResetCode = new Random().Next(100_000, 999_999);
            var ResetCodeExpiry = DateTime.UtcNow.AddMinutes(15);
            User.ResetCode = ResetCode;
            User.ResetCodeExpiry = ResetCodeExpiry;
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
