using Talabat.Core.Application.Abstraction.Models.Order;
using Talabat.Core.Application.Abstraction.Models.Orders;

namespace Talabat.Core.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order);
        Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId);
        Task<IEnumerable<OrderToReturnDto>> GetOrdersAsync(string buyerEmail);
        Task<IEnumerable<DeliverMethodDto>> GetDeliveryMethodsAsync();
    }
}
