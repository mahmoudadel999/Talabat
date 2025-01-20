using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence.Common;

namespace Talabat.Infrastructure.Persistence.Identity.Configurations
{
    [DbContextType(typeof(StoreIdentityDbContext))]
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.DisplayName).HasColumnType("varchar").HasMaxLength(100).IsRequired(true);

            builder
                .HasOne(U => U.Address)
                .WithOne(A => A.User)
                .HasForeignKey<Address>(A => A.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
