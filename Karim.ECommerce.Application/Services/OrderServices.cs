using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Shared.Dtos.Orders;
using Microsoft.AspNetCore.Identity;

namespace Karim.ECommerce.Application.Services
{
    internal class OrderServices(UserManager<ApplicationUser> userManager) : IOrderServices
    {
        public Task<OrderToReturnDto> CreateOrderAsync(OrderToCreateDto userOrder, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderToReturnDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<OrderToReturnDto> GetUserOrderByIdAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
