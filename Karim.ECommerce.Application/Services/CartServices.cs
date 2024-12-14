using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.Carts;
using Karim.ECommerce.Shared.Dtos.Carts;
using Karim.ECommerce.Shared.Exceptions;

namespace Karim.ECommerce.Application.Services
{
    internal class CartServices(ICartRepository cartRepository, IProductServices productServices, IMapper mapper) : ICartServices
    {
        public async Task<CartToReturnDto?> GetUserCart(string? cartId)
        {
            if (cartId is null) throw new BadRequestException("The CartId: {cartId} You Have Provid is not Valid");
            var cart = await cartRepository.GetCartAsync(cartId);
            if (cart is null || cart.CartItems is null) return null;
            //foreach (var Product in cart.CartItems)
            //{
            //    var RealProduct = await productServices.GetProductByIdAsync(Product.ProductId);
            //    Product = new CartItem()
            //    {
            //        ProductId = RealProduct.Id,
            //        ProductName = RealProduct.ProductName,
            //        PictureUrl = RealProduct.MainImage,
            //        Price = RealProduct.Price,
            //        Quantity = Product.Quantity,
            //        Brand = RealProduct.Brand,
            //        Category = RealProduct.Category
            //    };
            //}
            var mappedCart = mapper.Map<CartToReturnDto>(cart);
            return mappedCart;
        }
    }
}
