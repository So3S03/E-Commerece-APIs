using Karim.ECommerce.Domain.Contracts.Persistence;
using Karim.ECommerce.Domain.Entities._Base;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase;
using Microsoft.EntityFrameworkCore;

namespace Karim.ECommerce.Infrastructure.Persistence.GenaricRepository
{
    internal class GenaricRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenaricRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsyncWithNoSpecs()
            => await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsyncWithSpecs(ISpecifications<TEntity, TKey> specs)
            => await ApplySpecs(specs).ToListAsync();

        public async Task<int> GetItemsCountAsync(ISpecifications<TEntity, TKey> spec)
            => await ApplySpecs(spec).CountAsync();

        public async Task<TEntity?> GetAsyncWithNoSpecs(TKey id)
            => await dbContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetAsyncWithSpecs(ISpecifications<TEntity, TKey> specs)
            => await ApplySpecs(specs).FirstOrDefaultAsync();

        public async Task AddAsync(TEntity entity)
            => await dbContext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => dbContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => dbContext.Set<TEntity>().Remove(entity);

        private IQueryable<TEntity> ApplySpecs(ISpecifications<TEntity, TKey> specs)
            => SpecificationsEvaluator<TEntity, TKey>.GetQuery(dbContext.Set<TEntity>(), specs);


    }
}
