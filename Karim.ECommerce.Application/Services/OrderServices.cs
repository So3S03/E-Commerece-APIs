using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.Orders;
using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Domain.Specifications.Order;
using Karim.ECommerce.Shared.Dtos.Orders;
using Karim.ECommerce.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Karim.ECommerce.Application.Services
{
    internal class OrderServices(ICartServices cartServices, IUnitOfWork unitOfWork, IMapper mapper) : IOrderServices
    {
        public Task<OrderToReturnDto> CreateOrderAsync(OrderToCreateDto userOrder, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var OrderRepository = unitOfWork.GetRepository<Order, int>();
            var OrderSpec = new OrderSpecifications(buyerEmail);
            var AllUserOrder = await OrderRepository.GetAllAsyncWithSpecs(OrderSpec);
            if (AllUserOrder is null) throw new BadRequestException("Something Went Wrong While Retriving The Orders"); 
            var MappedOrders = mapper.Map<IEnumerable<OrderToReturnDto>>(AllUserOrder);
            return MappedOrders;
        }

        public async Task<OrderToReturnDto> GetUserOrderByIdAsync(string buyerEmail, int? orderId)
        {
            if (!orderId.HasValue) throw new BadRequestException("You Should Provid A Valid OrderId");
            var OrderRepo = unitOfWork.GetRepository<Order, int>();
            var OrderSpecs = new OrderSpecifications(buyerEmail, orderId.Value);
            var Order = await OrderRepo.GetAsyncWithSpecs(OrderSpecs);
            if(Order is null) throw new NotFoundException(nameof(Order), orderId);
            var MaappedOrder = mapper.Map<OrderToReturnDto>(Order);
            return MaappedOrder;
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync()
        {
            var DeliveryMethodRepository = unitOfWork.GetRepository<DeliveryMethod, int>();
            var DileveryMethodsList = await DeliveryMethodRepository.GetAllAsyncWithNoSpecs();
            if (DileveryMethodsList is null) throw new BadRequestException("Something Went Wrong While Retriving Delivery Methods Data");
            var MappedList = mapper.Map<IEnumerable<DeliveryMethodDto>>(DileveryMethodsList);
            return MappedList;
        }
    }
}
