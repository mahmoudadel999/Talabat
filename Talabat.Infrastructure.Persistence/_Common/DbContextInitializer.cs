using Talabat.Core.Domain.Contract.Persistence.DbContextInitializer;

namespace Talabat.Infrastructure.Persistence.Common
{
    public abstract class DbContextInitializer(DbContext dbContext) : IDbContextInitializer
    {
        public async Task InitializeAsync()
        {
            var pendingMigration = await dbContext.Database.GetAppliedMigrationsAsync();

            if (!pendingMigration.Any())
                await dbContext.Database.MigrateAsync();
        }

        public abstract Task SeedAsync();
    }
}
