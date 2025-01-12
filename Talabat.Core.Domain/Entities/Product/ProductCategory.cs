namespace Talabat.Core.Domain.Entities.Product
{
    public class ProductCategory : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
    }
}
