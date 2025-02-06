using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using Talabat.Core.Domain.Contract.Persistence;
using Talabat.Core.Domain.Entities.Basket;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Core.Domain.Specifications.Orders;
using Talabat.Shared.Exceptions;
using Talabat.Shared.Models;
using Talabat.Shared.Models.Basket;
using Product = Talabat.Core.Domain.Entities.Product.Product;

namespace Talabat.Infrastructure.Payment_Service
{
    internal class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptions<RedisSettings> redisoptions, IOptions<StripeSettings> options, ILogger<PaymentService> logger) : IPaymentService
    {
        private readonly RedisSettings _redisOptions = redisoptions.Value;
        private readonly StripeSettings _stripeOptions = options.Value;

        public async Task<CustomerBasketDto> CreateOrUpdatePaymnetIntent(string bsketId)
        {
            StripeConfiguration.ApiKey = _stripeOptions.SecretKey;

            var basket = await basketRepository.GetAsync(bsketId);
            if (basket is null) throw new NotFoundException(nameof(CustomerBasket), bsketId);

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.GetRepo<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                if (deliveryMethod is null) throw new NotFoundException(nameof(DeliveryMethod), basket.DeliveryMethodId.Value);
                basket.ShippingPrice = deliveryMethod!.Cost;
            }

            if (basket.Items.Count > 0)
            {
                var productRepo = unitOfWork.GetRepo<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);
                    if (product is null) throw new NotFoundException(nameof(Product), item.Id);

                    if (item.Price != product!.Price)
                        item.Price = product.Price;
                }
            }

            PaymentIntent? paymentIntent = null;
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "card" },
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            await basketRepository.UpdateAsync(basket, TimeSpan.FromDays(_redisOptions.TimeToLiveInDays));

            return mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task UpdateOrderStatus(string requestBody, string header)
        {
            var stripEvent = EventUtility.ConstructEvent(requestBody, header, _stripeOptions.WebHookSecret);

            var paymentIntent = (PaymentIntent)stripEvent.Data.Object;
            Order order;
            if (stripEvent.Type == "payment_intent.intent.succeeded")
            {
                order = await UpdatePaymentIntent(paymentIntent.Id, true);
                logger.LogInformation("Order is succeded with payment intent id {0}", paymentIntent.Id);
            }
            else if (stripEvent.Type == "payment_payment_failed")
            {
                order = await UpdatePaymentIntent(paymentIntent.Id, false);
                logger.LogInformation("Order is not succeded with payment intent id {0}", paymentIntent.Id);
            }
        }

        private async Task<Order> UpdatePaymentIntent(string id, bool isPaid)
        {
            var orderRepo = unitOfWork.GetRepo<Order, int>();
            var spec = new OrderWithPaymentIntentSpec(id);

            var order = await orderRepo.GetWithSpecAsync(spec);

            if (order is null) throw new NotFoundException(nameof(Order), $"PatmentIntentId: {id}");

            if (isPaid)
                order.Status = OrderStatus.PaymentReceived!;
            else
                order.Status = OrderStatus.PaymentFailed!;

            orderRepo.Update(order);

            await unitOfWork.CompleteAsync();

            return order;
        }


    }
}
