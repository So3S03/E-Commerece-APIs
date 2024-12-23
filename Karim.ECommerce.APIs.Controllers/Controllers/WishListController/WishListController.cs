using Karim.ECommerce.APIs.Controllers.Controllers._BaseController;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Dtos.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karim.ECommerce.APIs.Controllers.Controllers.WishListController
{
    [Authorize]
    public class WishListController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet("{WishListId}")]
        public async Task<ActionResult<WishListToReturnDto>> GetUserWishList(string WishListId)
        {
            var Result = await serviceManager.WishListServices.GetUserWishListAsync(WishListId);
            return Ok(Result);
        }

        [HttpPost]
        public async Task<ActionResult<WishListToReturnDto>> CreateOrUpdateUserWishList(WishListToCreateDto wishListToCreateDto)
        {
            var Result = await serviceManager.WishListServices.CreateUpdateWishListAsync(wishListToCreateDto);
            return Ok(Result);
        }

        [HttpDelete("{WishListId}")]
        public async Task<ActionResult<SuccessDto>> DeleteUserWishList(string WishListId)
        {
            var Result = await serviceManager.WishListServices.DeleteWishListAsync(WishListId);
            return Ok(Result);
        }

    }
}
