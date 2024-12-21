namespace Karim.ECommerce.Shared.Dtos.WishList
{
    public class WishListToReturnDto
    {
        public required string WishListId { get; set; }
        public ICollection<WishedProductsDto>? WishedProducts { get; set; }
    }
}
