using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence.Common;

namespace Talabat.Infrastructure.Persistence.Identity.Configurations
{
    [DbContextType(typeof(StoreIdentityDbContext))]
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.Property(A => A.Id).ValueGeneratedOnAdd(); 
            builder.Property(A => A.FirstName).HasMaxLength(50); 
            builder.Property(A => A.LastName).HasMaxLength(50); 
            builder.Property(A => A.Street).HasColumnType("varchar").HasMaxLength(50); 
            builder.Property(A => A.City).HasColumnType("varchar").HasMaxLength(50); 
            builder.Property(A => A.Country).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
