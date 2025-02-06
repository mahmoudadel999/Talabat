namespace Talabat.Core.Domain.Contract.Persistence.DbContextInitializer
{
    public interface IDbContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
