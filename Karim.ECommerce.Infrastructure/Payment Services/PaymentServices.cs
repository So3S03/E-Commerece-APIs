using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.Carts;
using Karim.ECommerce.Shared.Dtos.Carts;
using Karim.ECommerce.Shared.Exceptions;
using Stripe;

namespace Karim.ECommerce.Infrastructure.Payment_Services
{
    public class PaymentServices(ICartServices cartServices, IOrderServices orderServices) : IPaymentService
    {
        public async Task<CartToReturnDto?> CreateUpdatePaymentIntent(string cartId)
        {
            if (string.IsNullOrEmpty(cartId)) throw new BadRequestException("The Provided Cart Id Is Invalid");
            var UserCart = await cartServices.GetUserCartAsync(cartId);
            if (UserCart is null) throw new NotFoundException(nameof(Cart), cartId);
            if (UserCart.CartItems.Count <= 0) throw new BadRequestException("We Can't Update Or Create Payment Intent For Empty Cart");
            if(UserCart.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await orderServices.GetDeliveryMethodByIdAsync(UserCart.DeliveryMethodId.Value);
                UserCart.ShippingPrice = DeliveryMethod.Cost;
            }
            PaymentIntent? paymentIntent = null;
            PaymentIntentService? paymentIntentService = new PaymentIntentService();
            if(string.IsNullOrEmpty(UserCart.PaymentIntentId)) //Create New Payment Intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long) UserCart.CartItems.Sum(P => P.Price * 100 * P.Quantity) + (long) UserCart.ShippingPrice * 100,
                    Currency = "EGP",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await paymentIntentService.CreateAsync(options); //Integration With Stripe
                UserCart.PaymentIntentId = paymentIntent.Id;
                UserCart.ClientSecret = paymentIntent.ClientSecret;
            }
            else //Update Existing Payment Intent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)UserCart.CartItems.Sum(P => P.Price * 100 * P.Quantity) + (long)UserCart.ShippingPrice * 100
                };
                await paymentIntentService.UpdateAsync(UserCart.PaymentIntentId, options); //Integration With Stripe
            }

            var Created = await cartServices.UpdateUserCartAsync(UserCart);
            if (Created is null) throw new BadRequestException("Something Went Wrong While Updating The Payment Intent In The Cart");
            return UserCart;
        }
    }
}
