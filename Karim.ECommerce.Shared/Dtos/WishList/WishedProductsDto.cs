namespace Karim.ECommerce.Shared.Dtos.WishList
{
    public class WishedProductsDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public decimal Rating { get; set; }
    }
}
