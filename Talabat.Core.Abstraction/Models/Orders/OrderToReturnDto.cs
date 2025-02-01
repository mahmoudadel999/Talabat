﻿using Talabat.Core.Application.Abstraction.Models.Common;

namespace Talabat.Core.Application.Abstraction.Models.Orders
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; } 
        public required AddressDto ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? DeliveryMethod { get; set; }
        public virtual required ICollection<OrderItemDto>  Items { get; set; } 
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
