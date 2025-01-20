using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Domain.Common;
using Talabat.Infrastructure.Persistence.Data.Configurations.BaseEntity;

namespace Talabat.Infrastructure.Persistence.Data.Configurations.Base
{
    internal class BaseAuditableEntityConfiguration<TEntity, TKey> : BaseEntityConfiguration<TEntity, TKey>
        where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(E => E.CreatedOn).IsRequired()/*.HasDefaultValueSql("GETUTCDATE()")*/;
            builder.Property(E => E.CreatedBy).IsRequired();
            builder.Property(E => E.LastModifiedBy).IsRequired();
            builder.Property(E => E.LastModifiedOn).IsRequired()/*.HasDefaultValueSql("GETUTCDATE()")*/;
        }
    }
}
