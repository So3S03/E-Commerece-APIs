using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Infrastructure.Persistence._Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbType == typeof(StoreDbContext));
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryBrand> CategoryBrand { get; set; }
    }
}
