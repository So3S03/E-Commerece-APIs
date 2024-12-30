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

        [HttpPost("WebHock")]
        public async Task<ActionResult> WebHock()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await serviceManager.PaymentServices.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]!);
            return Ok();
        }
    }
}
