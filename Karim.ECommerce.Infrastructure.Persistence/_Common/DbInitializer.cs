using Karim.ECommerce.Domain._Common.Initializer;
using Microsoft.EntityFrameworkCore;

namespace Karim.ECommerce.Infrastructure.Persistence._Common
{
    public abstract class DbInitializer(DbContext dbContext) : IDbInitializer
    {
        public async Task DbInitializeAsync()
        {
            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
                await dbContext.Database.MigrateAsync();
        }

        public abstract Task SeedAsync();
    }
}
