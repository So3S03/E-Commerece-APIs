﻿namespace Karim.ECommerce.Domain.Entities.Carts
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
    }
}
