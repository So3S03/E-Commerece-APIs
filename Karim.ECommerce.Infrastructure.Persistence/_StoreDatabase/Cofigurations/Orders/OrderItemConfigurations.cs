using Karim.ECommerce.Domain.Entities.Orders;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations._Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations.Orders
{
    internal class OrderItemConfigurations : BaseAuditableEntityConfiguration<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.Property(Item => Item.Price)
                .HasColumnType("decimal(8,2)");
        }
    }
}
