using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.Carts;
using Karim.ECommerce.Domain.Entities.Orders;
using Karim.ECommerce.Domain.Specifications.Order;
using Karim.ECommerce.Shared.Dtos.Orders;
using Karim.ECommerce.Shared.Exceptions;
using DeliveryMethodEntity = Karim.ECommerce.Domain.Entities.Orders.DeliveryMethod;
namespace Karim.ECommerce.Application.Services
{
    internal class OrderServices(ICartServices cartServices, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService) : IOrderServices
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderToCreateDto userOrder, string buyerEmail)
        {
            if (userOrder is null) throw new BadRequestException("The Order You Try To Make Is Invalid");
            //1. Get User Cart
            var cart = await cartServices.GetUserCartAsync(userOrder.CartId);
            if (cart is null) throw new NotFoundException(nameof(Cart), userOrder.CartId);
            if (!cart.CartItems.Any()) throw new BadRequestException("Your Cart Is Empty, You Can't Create Order With Empty Cart");

            //2. Check On DeliveryMethod and Get It
            if (userOrder.DeliveryMethod <= 0) throw new BadRequestException("You Should Set Valid Value For Delivery Method");
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethodEntity, int>().GetAsyncWithNoSpecs(userOrder.DeliveryMethod);
            if (DeliveryMethod is null) throw new NotFoundException(nameof(DeliveryMethodEntity), userOrder.DeliveryMethod);


            //3. Create OrderItemList
            var ItemsList = new List<OrderItem>();
            foreach (var item in cart.CartItems)
            {
                var Item = new OrderItem()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    PictureUrl = item.PictureUrl!,
                    Quantity = item.Quantity
                };
                if (!ItemsList.Any(I => I.ProductId == Item.ProductId)) ItemsList.Add(Item);
            }


            //4. Calc SubTotal
            var SubTotal = ItemsList.Sum(P => P.Price *  P.Quantity);

            //5. Mapping The Address
            var Adderss = mapper.Map<OrderAddress>(userOrder.ShippingAddress);

            //6. Check If The Order Exist Or Not
            var OrderRepo = unitOfWork.GetRepository<Order, int>();
            var OrderSpec = new OrderSpecifications(cart.PaymentIntentId!, true);
            var ExistingOrder = await OrderRepo.GetAsyncWithSpecs(OrderSpec);
            if (ExistingOrder is not null)
            {
                OrderRepo.Delete(ExistingOrder);
                await paymentService.CreateUpdatePaymentIntent(cart.CartId);
            }

            //7. Craete The Order
            var OrderToBeCreate = new Order()
            {
                BuyerEmail = buyerEmail,
                Items = ItemsList,
                SubTotal = SubTotal,
                DeliveryMethod = DeliveryMethod,
                DeliveryMethodId = DeliveryMethod.Id,
                ShippingAddress = Adderss,
                PaymentIntentId = cart.PaymentIntentId!
            };

            await OrderRepo.AddAsync(OrderToBeCreate);

            //8. Save To Database
            var Created = await unitOfWork.CompleteAsync() > 0;
            if (!Created) throw new BadRequestException("Something Went Wrong While Creating The Order");

            //9. Mapping The Order To Return It
            var MappedOrder = mapper.Map<OrderToReturnDto>(OrderToBeCreate);
            return MappedOrder;

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

        public async Task<DeliveryMethodDto> GetDeliveryMethodByIdAsync(int deliveryMethodId)
        {
            if (deliveryMethodId <= 0) throw new BadRequestException("The Provided Delivery Method Id Is Invalid");
            var deliveryMethodRepo = unitOfWork.GetRepository<DeliveryMethodEntity, int>();
            var deliveryMethod = await deliveryMethodRepo.GetAsyncWithNoSpecs(deliveryMethodId);
            if(deliveryMethod is null) throw new NotFoundException(nameof(DeliveryMethodEntity), deliveryMethodId);
            var mappedMethod = mapper.Map<DeliveryMethodDto>(deliveryMethod);
            return mappedMethod;
        }
    }
}
