using Microsoft.EntityFrameworkCore.Diagnostics;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Domain.Common;

namespace Talabat.Infrastructure.Persistence.Data.Interceptors
{
    internal class CustomSaveChangesInterceptor(ILoginUserService _loginUserService) : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChanges(eventData, result);
        }

        private void UpdateEntities(DbContext? dbContext)
        {

            if (dbContext is null)
                return;
            foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>()
                .Where(entity => entity.State is EntityState.Added or EntityState.Modified))
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = _loginUserService.UserId!;
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }
                entry.Entity.LastModifiedBy = _loginUserService.UserId!;
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
            }


        }




    }

}
