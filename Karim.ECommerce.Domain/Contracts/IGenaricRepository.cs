using Karim.ECommerce.Domain.Entities._Base;

namespace Karim.ECommerce.Domain.Contracts
{
    public interface IGenaricRepository<TEntity,TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsyncWithNoSpecs();
        Task<IEnumerable<TEntity>> GetAllAsyncWithSpecs(ISpecifications<TEntity, TKey> specs);
        Task<int> GetItemsCountAsync(ISpecifications<TEntity, TKey> spec);
        Task<TEntity?> GetAsyncWithNoSpecs(TKey id);
        Task<TEntity?> GetAsyncWithSpecs(ISpecifications<TEntity, TKey> specs);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
