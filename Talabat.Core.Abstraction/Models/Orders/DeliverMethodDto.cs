﻿namespace Talabat.Core.Application.Abstraction.Models.Orders
{
    public class DeliverMethodDto
    {
        public int Id { get; set; }
        public required string ShortName { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
        public required string DeliveryTime { get; set; }
    }
}
