using Karim.ECommerce.APIs.Controllers.Controllers._BaseController;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Shared.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Karim.ECommerce.APIs.Controllers.Controllers.OrderController
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet("GetAllUserOrders")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllUserOrders()
        {
            var buyerEmail = User.FindFirst(ClaimTypes.Email)!.Value;
            var Result = await serviceManager.OrderServices.GetAllOrdersForUserAsync(buyerEmail);
            return Ok(Result);
        }

        [HttpGet("GetOrder/{orderId}")]
        public async Task<ActionResult<OrderToReturnDto>> GetUserOrder(int? orderId)
        {
            var buyerEmail = User.FindFirst(ClaimTypes.Email)!.Value;
            var Result = await serviceManager.OrderServices.GetUserOrderByIdAsync(buyerEmail, orderId);
            return Ok(Result);
        }

        [HttpPost("CraeteOrder")]
        public async Task<ActionResult<OrderToReturnDto>> CreateUserOrder(OrderToCreateDto order)
        {
            var buyerEmail = User.FindFirst(ClaimTypes.Email)!.Value;
            var Result = await serviceManager.OrderServices.CreateOrderAsync(order, buyerEmail);
            return Ok(Result);
        }

        [HttpGet("GetAllDeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetAllDeliveryMethods()
        {
            var Result = await serviceManager.OrderServices.GetAllDeliveryMethodsAsync();
            return Ok(Result);
        }

        [HttpGet("GetDeliveryMethodById/{id}")]
        public async Task<ActionResult<DeliveryMethodDto>> GetDeliveryMethod(int id)
        {
            var Result = await serviceManager.OrderServices.GetDeliveryMethodByIdAsync(id);
            return Ok(Result);
        }
    }
}
