using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Karim.ECommerce.Infrastructure.Persistence._StoreDatabase.Interceptors
{
    public class CustomSaveChangesInterceptor(ILoggedInUserService loggedInUser) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null) return;
            foreach (var item in dbContext.ChangeTracker.Entries<IBaseAuditableEntity>().Where(E => E.State is EntityState.Added or EntityState.Modified))
            {
                if (item.State is EntityState.Added)
                {
                    item.Entity.CreatedOn = DateTime.UtcNow;
                    item.Entity.CreatedBy = loggedInUser.UserId;
                }
                item.Entity.UpdatedOn = DateTime.UtcNow;
                item.Entity.UpdatedBy = loggedInUser.UserId;
            }
        }
    }
}
