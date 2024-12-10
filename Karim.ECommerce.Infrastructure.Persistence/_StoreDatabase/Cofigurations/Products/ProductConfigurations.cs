using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations._Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations.Products
{
    internal class ProductConfigurations : BaseAuditableEntityConfiguration<Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.Property(P => P.ProductName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(P => P.NormalizedName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(P => P.Description).HasColumnType("nvarchar(max)");
            builder.Property(P => P.Price).HasColumnType("decimal(8,2)").IsRequired();
            builder.Property(P => P.Rating).HasColumnType("decimal(2,1)").IsRequired();
            builder.Property(P => P.ImagesCollection).HasConversion
                (
                    (DataToStore) => JsonSerializer.Serialize(DataToStore, (JsonSerializerOptions)null!),
                    (DataToGet) => JsonSerializer.Deserialize<List<string>>(DataToGet, (JsonSerializerOptions)null!)
                )
                    .Metadata.SetValueComparer( new ValueComparer<List<string>>
                        (
                            (C01,C02) => C01!.SequenceEqual(C02!),
                            C => C.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            C => C.ToList()
                        )
                    );

            builder.HasOne(P => P.Brand)
                .WithMany(B => B.Products)
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(P => P.Category)
                .WithMany(C => C.Products)
                .HasForeignKey(P => P.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
