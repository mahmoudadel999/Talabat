using System.Reflection;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Entities.Product;
using Talabat.Infrastructure.Persistence.Common;

namespace Talabat.Infrastructure.Persistence.Data
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInfo).Assembly,
                type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreDbContext));
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in this.ChangeTracker.Entries<BaseAuditableEntity<int>>()
                .Where(entity => entity.State is EntityState.Added or EntityState.Modified))
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = "";
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }
                entry.Entity.LastModifiedBy = "";
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
    }
}
