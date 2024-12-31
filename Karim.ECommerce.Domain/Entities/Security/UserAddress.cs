namespace Karim.ECommerce.Domain.Entities.Security
{
    public class UserAddress
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; }
    }
}
