namespace Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string key, object response, TimeSpan timeToLive);
        Task<string?> GetCachedResponseAsync(string key); 
    }
}
