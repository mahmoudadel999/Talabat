using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Infrastructure.Persistence.Data.Configurations.Base;

namespace Talabat.Core.Domain.Entities.Orders
{
    internal class OrderConfigurations : BaseAuditableEntityConfiguration<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(order => order.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            builder
                .Property(order => order.Status)
                .HasConversion((orderStatus) => orderStatus.ToString(),
                orderStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus));


            builder.Property(order => order.Subtotal).HasColumnType("decimal(8, 2)");

            builder
                .HasOne(order => order.DeliveryMethod)
                .WithMany()
                .HasForeignKey(order => order.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(order => order.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
