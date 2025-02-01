using Microsoft.EntityFrameworkCore.Diagnostics;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Entities.Orders;

namespace Talabat.Infrastructure.Persistence.Data.Interceptors
{
    internal class AuditInterceptor(ILoginUserService _loginUserService) : SaveChangesInterceptor
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

            if (dbContext is null)
                return;

            var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
                .Where(entity => entity.State is EntityState.Added or EntityState.Modified);
            foreach (var entry in entries)
            {
                /// if (entry.Entity is Order or OrderItem)
                ///     _loginUserService.UserId = "";

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
