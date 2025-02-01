using Talabat.Core.Domain.Entities.Orders;

namespace Talabat.Core.Domain.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Order, int>
    {
        public OrderSpecifications(string buyerEmail) : base(order => order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
            AddOrderByDesc(order => order.OrderDate);
        }

        public OrderSpecifications(string buyerEmail, int id) : base(order => order.BuyerEmail == buyerEmail && order.Id == id)
        {
            AddIncludes();
            AddOrderByDesc(order => order.OrderDate);
        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(order => order.Items);
            Includes.Add(order => order.DeliveryMethod!);
        }
    }
}
