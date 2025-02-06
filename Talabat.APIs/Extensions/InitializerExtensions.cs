using Talabat.Core.Domain.Contract.Persistence.DbContextInitializer;

namespace Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeDbContextAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateAsyncScope();
            var storeDbContextInitializer = scope.ServiceProvider.GetRequiredService<IStoreDbContextInitializer>();
            var storeIdentityDbContextInitializer = scope.ServiceProvider.GetRequiredService<IStoreIdentityDbContextInitializer>();


            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            try
                {
                await storeDbContextInitializer.InitializeAsync();
                await storeDbContextInitializer.SeedAsync();

                await storeIdentityDbContextInitializer.InitializeAsync();
                await storeIdentityDbContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, $" {ex.Message} An error occurred while migrating the database or the data seed");
            }
            return app;
        }
    }
}
