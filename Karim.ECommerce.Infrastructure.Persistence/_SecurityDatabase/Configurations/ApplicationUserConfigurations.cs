using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karim.ECommerce.Infrastructure.Persistence._SecurityDatabase.Configurations
{
    [DbContextType(typeof(SecurityDbContext))]
    internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.DisplayName)
                .HasColumnType("nvarchar")
                .HasMaxLength(150)
                .IsRequired(true);

            builder.Property(U => U.UserName)
                .HasColumnType("nvarchar")
                .HasMaxLength(108)
                .IsRequired();

            builder.HasOne(U => U.Address)
                .WithOne(U => U.User)
                .HasForeignKey<UserAddress>(A => A.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
