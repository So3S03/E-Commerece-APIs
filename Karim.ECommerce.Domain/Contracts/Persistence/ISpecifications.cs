using Karim.ECommerce.Domain.Entities._Base;
using System.Linq.Expressions;

namespace Karim.ECommerce.Domain.Contracts.Persistence
{
    public interface ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } //For Where Clause --> ex: GetWithId(id)
        public List<string> IncludeStrings { get; set; } //For Include Clause --> For Nav Props
        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
