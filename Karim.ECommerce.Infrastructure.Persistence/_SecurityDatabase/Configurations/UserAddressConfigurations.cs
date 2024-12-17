using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.ECommerce.Infrastructure.Persistence._SecurityDatabase.Configurations
{
    [DbContextType(typeof(SecurityDbContext))]
    internal class UserAddressConfigurations : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.Property(UA => UA.Id).ValueGeneratedOnAdd();
            builder.Property(UA => UA.FirstName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(UA => UA.LastName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(UA => UA.Street).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(UA => UA.City).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(UA => UA.Country).HasColumnType("nvarchar").HasMaxLength(50);
        }
    }
}
