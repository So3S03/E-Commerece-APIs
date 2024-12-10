using Karim.ECommerce.Domain.Entities._Base;

namespace Karim.ECommerce.Domain.Entities.Products._Common
{
    public abstract class CommonProps<TKey> : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public required string NormalizedName { get; set; }
        public string? MainImage { get; set; }
    }
}
