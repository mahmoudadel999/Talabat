using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contract;
using Talabat.Core.Domain.Contract.Persistence;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence.Generic_Repository
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
            => WithTracking
                ? await _dbContext.Set<TEntity>().ToListAsync()
                : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, bool WithTracking = false)
            => await ApplySpec(spec).ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
            => await ApplySpec(spec).FirstOrDefaultAsync();

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
            => await ApplySpec(spec).CountAsync();
        
        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
           => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
           => _dbContext.Set<TEntity>().Remove(entity);

        #region Helper Methods

        private IQueryable<TEntity> ApplySpec(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_dbContext.Set<TEntity>(), spec);
        }

        #endregion
    }
}
