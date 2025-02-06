using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Domain.Entities.Identity;

namespace Talabat.Core.Application.Extensions
{
    internal static class UserManagerExtensions
    {
        public static async Task<ApplicationUser?> FindUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Where(user => user.Email == email).Include(user => user.Address).FirstOrDefaultAsync();

            return user;
        }
    }
}
