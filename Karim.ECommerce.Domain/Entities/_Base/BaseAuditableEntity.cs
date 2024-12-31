#nullable disable
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Contracts._Common;

namespace Karim.ECommerce.Domain.Entities._Base
{
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IBaseAuditableEntity
        where TKey : IEquatable<TKey>
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
