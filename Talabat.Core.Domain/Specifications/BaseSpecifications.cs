using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contract;

namespace Talabat.Core.Domain.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>>? criteriaExp)
        {
            Criteria = criteriaExp;
        }

        public BaseSpecifications(TKey id)
        {
            Criteria = E => E.Id.Equals(id);
        }

        private virtual protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp)
        {
            OrderBy = orderByExp;
        }
        private virtual protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByExpDesc)
        {
            OrderByDesc = orderByExpDesc;
        }


        private protected virtual void AddIncludes()
        {

        }

        private protected void ApplyPagination(int skip, int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }

    }
}
