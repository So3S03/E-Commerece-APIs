﻿using OrderEntity = Karim.ECommerce.Domain.Entities.Orders.Order;

namespace Karim.ECommerce.Domain.Specifications.Order
{
    public class OrderSpecifications : BaseSpecifications<OrderEntity, int>
    {
        public OrderSpecifications(string buyerEmail)
        {
            Criteria = O => O.BuyerEmail == buyerEmail;
            IncludesMethod();
            AddOrderByDesc(O => O.OrderDate);
        }

        public OrderSpecifications(string buyerEmail, int OrderId)
        {
            Criteria = O =>
                        (O.BuyerEmail == buyerEmail)
                            &&
                        (O.Id == OrderId);
            IncludesMethod();
        }

        private protected override void IncludesMethod()
        {
            base.IncludesMethod();
            IncludeStrings.Add($"{nameof(OrderEntity.Items)}");
            IncludeStrings.Add($"{nameof(OrderEntity.DeliveryMethod)}");
        }
    }
}