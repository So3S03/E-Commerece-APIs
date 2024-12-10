using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations._Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations.Products
{
    internal class CategoryConfigurations : BaseAuditableEntityConfiguration<Category,int>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(C => C.CategoryName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(C => C.NormalizedName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();


            builder.HasMany(C => C.Products)
                .WithOne(P => P.Category)
                .HasForeignKey(P => P.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(C => C.Brands)
                .WithOne(CB => CB.Category)
                .HasForeignKey(CB => CB.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
