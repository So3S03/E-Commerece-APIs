using Karim.ECommerce.Shared.Dtos.Security;

namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface IAuthServices
    {
        Task<UserDto> LoginAsync(LoginUserDto loginUserDto);
        Task<UserDto> RegisterAsync(RegisterUserDto registerUserDto);
    }
}
