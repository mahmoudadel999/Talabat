namespace Talabat.Core.Domain.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }

        // FK
        public required string UserId { get; set; }

        // Navigational Property
        public virtual required ApplicationUser User { get; set; }
    }
}
