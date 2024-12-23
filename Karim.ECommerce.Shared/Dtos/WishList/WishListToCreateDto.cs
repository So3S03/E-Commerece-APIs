namespace Karim.ECommerce.Shared.Dtos.WishList
{
    public class WishListToCreateDto
    {
        public required string WishListId { get; set; }
        public ICollection<WishedProductsIdDto>? WishedProductsId { get; set; }
    }
}
