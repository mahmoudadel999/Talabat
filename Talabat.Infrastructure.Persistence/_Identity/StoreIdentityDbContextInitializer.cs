using Microsoft.AspNetCore.Identity;
using Talabat.Core.Domain.Contract.Persistence.DbContextInitializer;
using Talabat.Core.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence.Common;

namespace Talabat.Infrastructure.Persistence.Identity
{
    internal class StoreIdentityDbContextInitializer(
        StoreIdentityDbContext dbContext,
        UserManager<ApplicationUser> userManager
        ) : DbContextInitializer(dbContext)
        , IStoreIdentityDbContextInitializer
    {
        public override async Task SeedAsync()
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    DisplayName = "Mahmoud Adel",
                    UserName = "Mahmoud.adel",
                    Email = "mahmoudadel2199@gmail.com",
                    PhoneNumber = "01153163140",
                };

                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
