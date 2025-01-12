using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contract;
using Talabat.Core.Domain.Entities.Product;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence.GenericRepositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return WithTracking
                    ? (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync()
                    : (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();
            }
            return WithTracking
                ? await _dbContext.Set<TEntity>().ToListAsync()
                : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
                return await _dbContext.Set<Product>().Where(P => P.Id.Equals(id)).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as TEntity;

            return await _dbContext.Set<TEntity>().FindAsync(id);
        }


        public async Task AddAsync(TEntity entity)
            => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Update(TEntity entity)
           => _dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _dbContext.Set<TEntity>().Remove(entity);
    }
}
