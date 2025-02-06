using AutoMapper;
using Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using Talabat.Core.Application.Abstraction.Models.Order;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Application.Abstraction.Services.Orders;
using Talabat.Core.Domain.Contract.Persistence;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Core.Domain.Entities.Product;
using Talabat.Core.Domain.Specifications.Orders;
using Talabat.Shared.Exceptions;

namespace Talabat.Core.Application.Services.Orders
{
    internal class OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketService basketService, IPaymentService paymentService) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            // Get basket from basket repos
            var basket = await basketService.GetCustomerBasketAsync(order.BasketId);

            var orderItems = new List<OrderItem>();

            if (basket.Items.Count > 0)
            {
                var productRepo = unitOfWork.GetRepo<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);

                    if (product != null)
                    {
                        var productItemOrder = new ProductItemOrder()
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PictureUrl ?? "",
                        };

                        var orderItem = new OrderItem()
                        {
                            Product = productItemOrder,
                            Price = product.Price,
                            Quantity = item.Quantity,
                        };

                        orderItems.Add(orderItem);
                    }
                }
            }

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            var address = mapper.Map<Address>(order.ShippingAddress);

            var deliveryMethod = await unitOfWork.GetRepo<DeliveryMethod, int>().GetAsync(order.DeliveryMethodId);

            var orderRepo = unitOfWork.GetRepo<Order, int>();

            var orderSpec = new OrderWithPaymentIntentSpec(basket.PaymentIntentId!);

            var existOrder = await orderRepo.GetWithSpecAsync(orderSpec);

            if (existOrder is not null)
            {
                orderRepo.Delete(existOrder);
                await paymentService.CreateOrUpdatePaymnetIntent(basket.Id);
            }

            var CreatedOrder = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                Items = orderItems,
                Subtotal = subTotal,
                DeliveryMethod = deliveryMethod,
                PaymentIntentId = basket.PaymentIntentId!,
            };

            await orderRepo.AddAsync(CreatedOrder);

            var createdOrder = await unitOfWork.CompleteAsync() > 0;

            if (!createdOrder)
                throw new BadRequestException("An error has been occured during creating the order");

            return mapper.Map<OrderToReturnDto>(CreatedOrder);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetOrdersAsync(string buyerEmail)
        {
            var orderSpec = new OrderSpecifications(buyerEmail);

            var orders = await unitOfWork.GetRepo<Order, int>().GetAllWithSpecAsync(orderSpec);

            return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var orderSpec = new OrderSpecifications(buyerEmail, orderId);
            var order = await unitOfWork.GetRepo<Order, int>().GetWithSpecAsync(orderSpec);
            if (order is null)
                throw new NotFoundException(nameof(Order), orderId);

            return mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliverMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliverMethod = await unitOfWork.GetRepo<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliverMethodDto>>(deliverMethod);
        }
    }
}
