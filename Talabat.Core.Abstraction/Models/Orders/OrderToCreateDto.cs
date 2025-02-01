using Talabat.Core.Application.Abstraction.Models.Common;

namespace Talabat.Core.Application.Abstraction.Models.Order
{
    public class OrderToCreateDto
    {
        public required string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public required AddressDto ShippingAddress { get; set; }
    }
}
