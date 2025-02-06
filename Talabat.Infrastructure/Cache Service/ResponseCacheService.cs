using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;

namespace Talabat.Infrastructure.Cache_Service
{
    internal class ResponseCacheService(IConnectionMultiplexer connectionMultiplexer) : IResponseCacheService
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null) return;

            var serlizeOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serlizedResponse = JsonSerializer.Serialize(response, serlizeOptions);

            await _database.StringSetAsync(key, serlizedResponse, timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string key)
        {
            var response = await _database.StringGetAsync(key);

            if (response.IsNull) return null;

            return response;
        }
    }
}
