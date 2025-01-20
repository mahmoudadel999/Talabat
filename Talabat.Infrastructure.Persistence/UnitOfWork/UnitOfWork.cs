using System.Collections.Concurrent;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contract.Persistence;
using Talabat.Infrastructure.Persistence.Data;
using Talabat.Infrastructure.Persistence.Generic_Repository;

namespace Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> _repository = new();

        public IGenericRepository<TEntity, TKey> GetRepo<TEntity, TKey>()
            where TEntity : BaseAuditableEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenericRepository<TEntity, TKey>)_repository.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(dbContext));
        }

        public async Task<int> CompleteAsync() => await dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
    }
}
