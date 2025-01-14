using System.Security.Claims;
using Talabat.Core.Application.Abstraction;

namespace Talabat.APIs.Services
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string? UserId { get; }

        public LoginUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
        }
    }
}
