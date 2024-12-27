using Karim.ECommerce.Domain.Entities.Orders;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations._Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations.Orders
{
    internal class OrderConfigurations : BaseAuditableEntityConfiguration<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(O => O.ShippingAddress, shippingAddress => shippingAddress.WithOwner());
            builder.Property(O => O.Status)
                .HasConversion(
                    (OStatus) => OStatus.ToString(),
                    (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                );
            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(8,2)");
            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .HasForeignKey(O => O.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(O => O.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
