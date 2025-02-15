﻿namespace Talabat.Core.Domain.Entities.Orders
{
    public class OrderItem : BaseAuditableEntity<int>
    {

        public required ProductItemOrder Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
