using Karim.ECommerce.Domain.Entities._Base;

namespace Karim.ECommerce.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>;

        ValueTask SaveChangesAsync();
    }
}
