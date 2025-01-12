using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Domain.Entities.Product;
using Talabat.Infrastructure.Persistence.Data.Configurations.Base;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Products
{
    internal class ProductConfiguration : BaseAuditableEntityConfiguration<Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.Price).HasColumnType("decimal(9, 2)");

            builder
                .HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(P => P.Category)
                .WithMany()
                .HasForeignKey(P => P.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
