using Karim.ECommerce.Shared.Dtos.Orders;

namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface IOrderServices
    {
        Task<OrderToReturnDto> GetUserOrderByIdAsync(string buyerEmail, int? orderId);
        Task<OrderToReturnDto> CreateOrderAsync(OrderToCreateDto userOrder, string buyerEmail);
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersForUserAsync(string buyerEmail);
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync();
        Task<DeliveryMethodDto> GetDeliveryMethodByIdAsync(int deliveryMethodId);
    }
}
