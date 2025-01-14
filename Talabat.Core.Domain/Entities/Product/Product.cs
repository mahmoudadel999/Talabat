namespace Talabat.Core.Domain.Entities.Product
{
    public class Product : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string NormalizedName { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        // FK ->> ProductBrand [Entity]
        public int? BrandId { get; set; }

        // Navigational Property
        public virtual ProductBrand? Brand { get; set; }

        // FK ->> ProductCategory [Entity]
        public int? CategoryId { get; set; }

        // Navigational Property
        public virtual ProductCategory? Category { get; set; }
    }
}
