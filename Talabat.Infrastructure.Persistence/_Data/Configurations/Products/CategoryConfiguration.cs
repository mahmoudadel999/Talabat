using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Domain.Entities.Product;
using Talabat.Infrastructure.Persistence.Data.Configurations.Base;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Products
{
    internal class CategoryConfiguration : BaseAuditableEntityConfiguration<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(C => C.Name).IsRequired();
        }
    }
}
