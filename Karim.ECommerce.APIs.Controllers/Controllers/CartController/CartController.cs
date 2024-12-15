using Karim.ECommerce.APIs.Controllers.Controllers._BaseController;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Shared.Dtos.Carts;
using Microsoft.AspNetCore.Mvc;

namespace Karim.ECommerce.APIs.Controllers.Controllers.CartController
{
    public class CartController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet("{cartId}")]
        public async Task<ActionResult<CartToReturnDto>> GetUserCart(string cartId)
        {
            var cart = await serviceManager.CartServices.GetUserCartAsync(cartId);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CartToReturnDto>> UpdateUserCart(CartToReturnDto NewCart)
        {
            var cart = await serviceManager.CartServices.UpdateUserCartAsync(NewCart);
            return Ok(cart);
        }

        [HttpDelete("{cartId}")]
        public async Task<ActionResult> ClearUserCart(string cartId)
        {
            await serviceManager.CartServices.ClearUserCartAsync(cartId);
            return Ok(new {message = "Your Cart Has Been Cleared" });
        }
    }
}
