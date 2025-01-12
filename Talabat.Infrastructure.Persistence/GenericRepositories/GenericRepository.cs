using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contract;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence.GenericRepositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
            => WithTracking
            ? await _dbContext.Set<TEntity>().ToListAsync()
            : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id)
            => await _dbContext.Set<TEntity>().FindAsync(id);


        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Update(TEntity entity)
           => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);
    }
}
