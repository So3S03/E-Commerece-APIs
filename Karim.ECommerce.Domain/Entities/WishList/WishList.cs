namespace Karim.ECommerce.Domain.Entities.WishList
{
    public class WishList
    {
        public required string WishListId { get; set; }
        public ICollection<WishedProduct>? WishedProductsId { get; set; }
        
    }
}
