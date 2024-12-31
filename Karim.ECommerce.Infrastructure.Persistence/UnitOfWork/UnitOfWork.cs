using Karim.ECommerce.Domain.Contracts.Persistence;
using Karim.ECommerce.Domain.Entities._Base;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase;
using Karim.ECommerce.Infrastructure.Persistence.GenaricRepository;
using System.Collections.Concurrent;

namespace Karim.ECommerce.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> Repos = new ConcurrentDictionary<string, object>();

        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
            => (IGenaricRepository<TEntity,TKey>) Repos.GetOrAdd(typeof(TEntity).Name, new GenaricRepository<TEntity, TKey>(dbContext));

        public async Task<int> CompleteAsync()
            => await dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await dbContext.DisposeAsync();
    }
}
