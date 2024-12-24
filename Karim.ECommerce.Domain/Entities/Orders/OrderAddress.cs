namespace Karim.ECommerce.Domain.Entities.Orders
{
    public class OrderAddress
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
    }
}
