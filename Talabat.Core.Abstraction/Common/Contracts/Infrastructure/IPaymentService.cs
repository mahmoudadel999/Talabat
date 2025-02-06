using Talabat.Shared.Models.Basket;

namespace Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;
public interface IPaymentService
{
    Task<CustomerBasketDto> CreateOrUpdatePaymnetIntent(string bsketId);
    Task UpdateOrderStatus(string requestBody, string header);
}

