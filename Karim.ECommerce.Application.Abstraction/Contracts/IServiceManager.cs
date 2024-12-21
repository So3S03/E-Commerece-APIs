﻿namespace Karim.ECommerce.Application.Abstraction.Contracts
{
    public interface IServiceManager
    {
        public IProductServices ProductServices { get; }
        public ICartServices CartServices { get; }
        public IAuthServices AuthServices { get; }
        public IWishListServices WishListServices { get; }
    }
}
