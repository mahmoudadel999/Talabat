using Talabat.Core.Domain.Entities.Basket;

namespace Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetAsync(string id);
    Task<CustomerBasket?> UpdateAsync(CustomerBasket customerBasket, TimeSpan timeToLive);
    Task<bool> DeleteAsync(string id);
}
