using Karim.ECommerce.APIs.Controllers.Controllers._BaseController;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Shared.Dtos.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karim.ECommerce.APIs.Controllers.Controllers.PaymentController
{
    public class PaymentController(IServiceManager serviceManager) : ApiControllerBase
    {
        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<CartToReturnDto>> CraeteOrUpdatePaymentIntent(string cartId)
        {
            var Result = await serviceManager.PaymentServices.CreateUpdatePaymentIntent(cartId);
            return Ok(Result);
        }
    }
}
