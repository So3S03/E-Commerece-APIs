using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Domain.Entities.Carts;
using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Domain.Specifications.Product;
using Karim.ECommerce.Shared.AppSettingsModels;
using Karim.ECommerce.Shared.Dtos.Carts;
using Karim.ECommerce.Shared.Exceptions;
using Microsoft.Extensions.Options;

namespace Karim.ECommerce.Application.Services
{
    internal class CartServices(ICartRepository cartRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptions<RedisSettings> redisSettings) : ICartServices
    {
        private readonly RedisSettings _redisSettings = redisSettings.Value;
        public async Task<CartToReturnDto> GetUserCartAsync(string? cartId)
        {
            if (cartId is null) throw new BadRequestException("The CartId: {cartId} You Have Provid is not Valid");
            var cart = await cartRepository.GetCartAsync(cartId);
            if (cart is null) throw new NotFoundException(nameof(Cart), cartId);
            var mappedCart = mapper.Map<CartToReturnDto>(cart);
            return mappedCart;
        }

        public async Task<CartToReturnDto?> UpdateUserCartAsync(CartToReturnDto? cartToReturnDto)
        {
            var cart = mapper.Map<Cart>(cartToReturnDto);
            //I Can't Relay On The Data That Came From Client So I Will Trust Only (ProductId, Quantity) To ReCreate The CartItems
            var RealProducts = new List<CartItem>();
            var ProductSpec = new ProductWithBrandAndCategorySpecs();
            var ProductsList = await unitOfWork.GetRepository<Product, int>().GetAllAsyncWithSpecs(ProductSpec);
;            foreach (var Product in cart.CartItems)
            {
                var RealProduct = ProductsList.Where(P => P.Id == Product.ProductId).FirstOrDefault();
                var cartItem = new CartItem()
                {
                    ProductId = RealProduct!.Id,
                    ProductName = RealProduct.ProductName,
                    PictureUrl = RealProduct.MainImage,
                    Price = RealProduct.Price,
                    Quantity = Product.Quantity,
                    Brand = RealProduct.Brand!.BrandName,
                    Category = RealProduct.Category!.CategoryName
                };
                RealProducts.Add(cartItem);
            }
            cart.CartItems = RealProducts;
            var updatedCart = await cartRepository.UpdateCartAsync(cart, TimeSpan.FromDays(_redisSettings.CartExpiredTimeSpan));
            if (updatedCart is null) throw new BadRequestException("Cannot Update Your Cart, Something Went Wrong");
            return mapper.Map<CartToReturnDto>(cart);
        }

        public async Task ClearUserCartAsync(string? cartId)
        {
            if (cartId is null) throw new BadRequestException($"The Cart With Id: {cartId} You Try To Reach Is Not Exist");
            var DeletedCart = await cartRepository.ClearCartAsync(cartId);
            if (!DeletedCart) throw new BadRequestException($"Something Went Wrong While Clearing Cart With Id: {cartId}");
        }
    }
}
