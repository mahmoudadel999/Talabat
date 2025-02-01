using System.Security.Claims;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Models.Common;

namespace Talabat.Core.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);
        Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal);
        Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal,AddressDto model);
        Task<bool> CheckEmail(string email);
    }
}
