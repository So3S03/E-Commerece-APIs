﻿using Karim.ECommerce.Domain.Entities.WishList;

namespace Karim.ECommerce.Domain.Contracts.Infrastructure
{
    public interface IWishListRepository
    {
        Task<WishList?> GetWishListAsync(string WishListId);
        Task<WishList?> CreateUpdateWishListAsync(WishList wishList);
        Task<bool> DeleteWishListAsync(string WishListId);
    }
}
