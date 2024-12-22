using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Dtos.WishList;

namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface IWishListServices
    {
        Task<WishListToReturnDto> GetUserWishListAsync(string wishListId);
        Task<WishListToReturnDto> CreateUpdateWishListAsync(WishListToCreateDto wishListToCreateDto);
        Task<SuccessDto> DeleteWishListAsync(string wishListId);
    }
}
