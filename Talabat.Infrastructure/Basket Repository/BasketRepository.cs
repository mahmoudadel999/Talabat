using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using Talabat.Core.Domain.Entities.Basket;

namespace Talabat.Infrastructure.Basket_Repository
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket customerBasket, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(customerBasket);
            var updated = await _database.StringSetAsync(customerBasket.Id, value, timeToLive);

            if (updated) return customerBasket;
            return null;
        }

        public async Task<bool> DeleteAsync(string id)
            => await _database.KeyDeleteAsync(id);


    }
}
