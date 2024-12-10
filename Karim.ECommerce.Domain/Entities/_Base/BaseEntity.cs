#nullable disable
namespace Karim.ECommerce.Domain.Entities._Base
{
    public abstract class BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
