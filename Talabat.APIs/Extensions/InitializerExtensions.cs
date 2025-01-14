using Talabat.Core.Domain.Contract.Persistence;

namespace Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeStoreDbContextAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateAsyncScope();
            var storeDbContextInitializer = scope.ServiceProvider.GetRequiredService<IStoreDbContextInitializer>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            try
            {
                await storeDbContextInitializer.InitializeAsync();
                await storeDbContextInitializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating the database or the data seed");
            }
            return app;
        }
    }
}
