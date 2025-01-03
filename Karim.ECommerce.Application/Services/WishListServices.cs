﻿using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Domain.Contracts.Infrastructure;
using Karim.ECommerce.Domain.Contracts.Persistence;
using Karim.ECommerce.Domain.Entities.WishList;
using Karim.ECommerce.Shared.Dtos.Carts;
using Karim.ECommerce.Shared.Dtos.Common;
using Karim.ECommerce.Shared.Dtos.WishList;
using Karim.ECommerce.Shared.Exceptions;
using ProductEntity = Karim.ECommerce.Domain.Entities.Products.Product;
using WishListEntity = Karim.ECommerce.Domain.Entities.WishList.WishList;

namespace Karim.ECommerce.Application.Services
{
    public class WishListServices(IWishListRepository wishListRepository, IUnitOfWork unitOfWork, ICartServices cartServices) : IWishListServices
    {
        public async Task<WishListToReturnDto> CreateUpdateWishListAsync(WishListToCreateDto wishListToCreateDto)
        {
            if (wishListToCreateDto is null) throw new BadRequestException("The Wish List You Provided Is Invalid");
            if(wishListToCreateDto.WishedProductsId is null) throw new BadRequestException("The Wish List Ids That You Try To Create Or Update Is Empty");
            //1. To Store
            var WishedProductsListToCreate = new List<WishedProduct>();

            //2. To Show
            var WishedProductsListToReturn = new List<WishedProductsDto>();

            var ProductsList = await unitOfWork.GetRepository<ProductEntity, int>().GetAllAsyncWithNoSpecs();

            foreach(var IdVariable in wishListToCreateDto.WishedProductsId)
            {
                //1. To Store
                var WishedProduct = new WishedProduct()
                {
                    ProductId = IdVariable.ProductId
                };
                if(!WishedProductsListToCreate.Any(P => P.ProductId == WishedProduct.ProductId)) WishedProductsListToCreate.Add(WishedProduct);

                //2. To Show
                var Product = ProductsList.Where(P => P.Id == IdVariable.ProductId).FirstOrDefault();
                if (Product is null) throw new NotFoundException(nameof(ProductEntity), IdVariable.ProductId);
                var MappedProduct = new WishedProductsDto()
                {
                    ProductId = Product.Id,
                    ProductName = Product.ProductName,
                    PictureUrl = Product.MainImage,
                    InStock = Product.QuantityInStock > 0 ? true : false,
                    Price = Product.Price,
                    Rating = Product.Rating,
                };
                if (!WishedProductsListToReturn.Any(P => P.ProductId == MappedProduct.ProductId)) WishedProductsListToReturn.Add(MappedProduct);
            }
            //1. To Store
            var WishListToStore = new WishListEntity()
            {
                WishListId = wishListToCreateDto.WishListId,
                WishedProductsId = WishedProductsListToCreate
            };
            var Result = await wishListRepository.CreateUpdateWishListAsync(WishListToStore);
            if (Result is null) throw new BadRequestException("Something Went Wrong While Creating Your Wish List");

            //2. To Show
            var WishListToShow = new WishListToReturnDto()
            {
                WishListId = wishListToCreateDto.WishListId,
                WishedProducts = WishedProductsListToReturn
            };
            
            return WishListToShow;
        }

        public async Task<SuccessDto> DeleteWishListAsync(string wishListId)
        {
            if (string.IsNullOrEmpty(wishListId)) throw new BadRequestException($"The Wish List Id : {wishListId} You Have Provided Is Invalid");
            var WishList = await wishListRepository.GetWishListAsync(wishListId);
            if (WishList is null) throw new NotFoundException(nameof(WishListEntity), wishListId);
            var Deleted = await wishListRepository.DeleteWishListAsync(wishListId);
            if (!Deleted) throw new BadRequestException("Something Went Wrong While Deleting The Wish List");
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "Wish List Deleted Successfully"
            };
            return SuccessObj;
        }

        public async Task<WishListToReturnDto> GetUserWishListAsync(string wishListId)
        {
            if (string.IsNullOrEmpty(wishListId)) throw new BadRequestException($"The Provided Wish List Id: {wishListId} Is Invalid");
            var RetrivedWishList = await wishListRepository.GetWishListAsync(wishListId);
            if (RetrivedWishList is null) throw new NotFoundException(nameof(WishListEntity), wishListId);
            var WishedProducts = new List<WishedProductsDto>();
            var ProductsList = await unitOfWork.GetRepository<ProductEntity, int>().GetAllAsyncWithNoSpecs();
            foreach(var IdsVariable in RetrivedWishList.WishedProductsId!)
            {
                var Product = ProductsList.Where(P => P.Id == IdsVariable.ProductId).FirstOrDefault();
                var MappedProduct = new WishedProductsDto()
                {
                    ProductId = Product!.Id,
                    ProductName = Product.ProductName,
                    PictureUrl = Product.MainImage,
                    InStock = Product.QuantityInStock > 0 ? true : false,
                    Price = Product.Price,
                    Rating = Product.Rating,
                };
                WishedProducts.Add(MappedProduct);
            }
            var WishList = new WishListToReturnDto()
            {
                WishListId = wishListId,
                WishedProducts = WishedProducts
            };
            return WishList;
        }

        public async Task<SuccessDto> AddAllWishedProductToCartAsync(string wishListId, string cartId)
        {
            if (string.IsNullOrEmpty(wishListId)) throw new BadRequestException("Please Provide A Valid wish List Id");
            var UserWishList = await GetUserWishListAsync(wishListId);
            if (UserWishList is null) throw new NotFoundException(nameof(WishListEntity), wishListId);
            if (UserWishList.WishedProducts is null) throw new BadRequestException("Your Wished Products List Is Empty You Can't Add Empty List To Your Cart");
            var Items = new List<CartItemDto>();
            foreach(var item in UserWishList.WishedProducts)
            {
                var WishedProduct = new CartItemDto()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = 1,
                    PictureUrl = item.PictureUrl,
                    Brand = "",
                    Category = ""
                };
                Items.Add(WishedProduct);
            }
            var cartToCreate = new CartToReturnDto()
            {
                CartId = cartId,
                CartItems = Items
            };
            var Sent = await cartServices.UpdateUserCartAsync(cartToCreate) is null;
            if (Sent) throw new BadRequestException("Something Went Wrong While Adding The Wish List To The Cart");
            await DeleteWishListAsync(wishListId);
            var SuccessObj = new SuccessDto()
            {
                Status = "Success",
                Message = "Wish List Added Successfully To The Cart"
            };
            return SuccessObj;
        }
    }
}
