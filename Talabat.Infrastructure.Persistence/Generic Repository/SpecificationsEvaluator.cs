using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;

namespace Talabat.Infrastructure.Persistence.Generic_Repository
{
    internal static class SpecificationsEvaluator<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            else if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.IsPaginationEnable == true)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (curr, includeExp) => curr.Include(includeExp));

            return query;
        }
    }
}
