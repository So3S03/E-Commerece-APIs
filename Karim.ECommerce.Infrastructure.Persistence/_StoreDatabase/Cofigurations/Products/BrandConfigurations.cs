using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations._Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations.Products
{
    internal class BrandConfigurations : BaseAuditableEntityConfiguration<Brand, int>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            base.Configure(builder);
            builder.Property(C => C.BrandName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(C => C.NormalizedName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();

            builder.HasMany(B => B.Products)
                .WithOne(P => P.Brand)
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(B => B.Categories)
                .WithOne(CB => CB.Brand)
                .HasForeignKey(CB => CB.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
