using Karim.ECommerce.Domain.Contracts.Persistence;
using Karim.ECommerce.Domain.Entities._Base;
using Microsoft.EntityFrameworkCore;

namespace Karim.ECommerce.Infrastructure.Persistence.GenaricRepository
{
    internal static class SpecificationsEvaluator<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications)
        {
            var Query = inputQuery; //_dbContext.Set<Entity>()

            if (specifications.Criteria is not null) //Expression For Where Clause
                Query = Query.Where(specifications.Criteria); //Ex:- _dbContext.Set<Entity>().Where(E => E.Id.Equals(Id))


            if (specifications.IncludeStrings.Count > 0) //Expression For Include Clause
                Query = specifications.IncludeStrings
                    .Aggregate(Query, (CurrentQuery, Include) => CurrentQuery.Include(Include)); //Ex:- _dbContext.Set<Entity>().Where(E => E.Id.Equals(Id)).Include(E => E.Brands);


                if (specifications.OrderByDesc is not null)
                Query = Query.OrderByDescending(specifications.OrderByDesc);
            else if(specifications.OrderBy is not null)
                Query = Query.OrderBy(specifications.OrderBy);

            if(specifications.IsPaginationEnabled)
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);

            return Query;
        }
    }
}
