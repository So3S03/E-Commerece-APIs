using Karim.ECommerce.Domain.Contracts.Persistence;
using Karim.ECommerce.Domain.Entities._Base;
using System.Linq.Expressions;

namespace Karim.ECommerce.Domain.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
        public List<string> IncludeStrings { get; set; } = new();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get ; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecifications() // For Getting All Entites
        {
            
        }

        public BaseSpecifications(TKey Id) //For Getting A Specific Entity
        {
            Criteria = E => E.Id.Equals(Id);
        }

        private protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        private protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        private protected virtual void IncludesMethod() {}
        private protected void ApplyPagination(int Skip, int Take) 
        {
            IsPaginationEnabled = true;
            this.Skip = Skip;
            this.Take = Take;
        }
    }
}
