using Talabat.Core.Domain.Entities.Orders;

namespace Talabat.Core.Domain.Specifications.Orders
{
    public class OrderWithPaymentIntentSpec : BaseSpecifications<Order, int>
    {
        public OrderWithPaymentIntentSpec(string paymentId) : base(order => order.PaymentIntentId == paymentId)
        {

        }
    }
}
