using Karim.ECommerce.Domain.Entities._Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations._Base
{
    internal class BaseAuditableEntityConfiguration<TEntity, TKey> : BaseEntityConfigurations<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(E => E.CreatedBy).IsRequired();
            builder.Property(E => E.CreatedOn).IsRequired();
            builder.Property(E => E.UpdatedBy).IsRequired();
            builder.Property(E => E.UpdatedOn).IsRequired();
        }
    }
}
