using Talabat.Core.Application.Abstraction.Models.Auth;

namespace Talabat.Core.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);
    }
}
