using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Infrastructure.Persistence.Data.Configurations.Base;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Orders
{
    internal class OrderItemConfigurations : BaseAuditableEntityConfiguration<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(item => item.Product, product => product.WithOwner());

            builder.Property(item => item.Price).HasColumnType("decimal(8, 2)");
        }
    }
}
