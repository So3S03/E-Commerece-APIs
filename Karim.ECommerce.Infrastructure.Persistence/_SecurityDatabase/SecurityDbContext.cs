using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Karim.ECommerce.Infrastructure.Persistence._SecurityDatabase
{
    public class SecurityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), 
                type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbType == typeof(SecurityDbContext));
        }

        public DbSet<UserAddress> Addresses { get; set; }
    }
}
