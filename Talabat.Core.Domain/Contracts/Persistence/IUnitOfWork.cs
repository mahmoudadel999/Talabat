namespace Talabat.Core.Domain.Contract.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity, TKey> GetRepo<TEntity, TKey>()
            where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();
    }
}
