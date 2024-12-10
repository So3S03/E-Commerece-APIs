using Karim.ECommerce.Domain.Entities._Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Interceptors
{
    public class CustomSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null) return;
            foreach (var item in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>().Where(E => E.State is EntityState.Added or EntityState.Modified))
            {
                if (item.State is EntityState.Added)
                {
                    item.Entity.CreatedOn = DateTime.UtcNow;
                    item.Entity.CreatedBy = "1";
                }
                item.Entity.UpdatedOn = DateTime.UtcNow;
                item.Entity.UpdatedBy = "1";
            }
        }
    }
}
