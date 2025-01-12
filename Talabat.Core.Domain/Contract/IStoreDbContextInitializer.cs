namespace Talabat.Core.Domain.Contract
{
    public interface IStoreDbContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
