using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Infrastructure.Persistence.Data.Configurations.BaseEntity;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Orders
{
    internal class DeliveryMethodConfigurations : BaseEntityConfiguration<DeliveryMethod, int>
    {
        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);

            builder.Property(method => method.Cost).HasColumnType("decimal(8, 2)");
        }
    }
}
