using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Cofigurations.Products
{
    [DbContextType(typeof(StoreDbContext))]
    internal class CategoryBrandConfigurations : IEntityTypeConfiguration<CategoryBrand>
    {
        public void Configure(EntityTypeBuilder<CategoryBrand> builder)
        {
            builder.Property(CB => CB.BrandId).IsRequired(false);
            builder.Property(CB => CB.CategoryId).IsRequired(false);
            builder.HasKey(CB => new {CB.CategoryId, CB.BrandId});
        }
    }
}
