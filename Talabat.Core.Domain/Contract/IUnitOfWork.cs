using Talabat.Core.Domain.Entities.Product;

namespace Talabat.Core.Domain.Contract
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<TEntity, TKey> GetRepos<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();
    }
}
