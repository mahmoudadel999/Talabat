namespace Talabat.Core.Domain.Contract.Persistence
{
    public interface IStoreDbContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
