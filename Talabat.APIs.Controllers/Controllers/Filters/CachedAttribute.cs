using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;

namespace Talabat.APIs.Controllers.Controllers.Filters
{
    internal class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveWithSeconds;

        public CachedAttribute(int timeToLiveWithSeconds)
        {
            _timeToLiveWithSeconds = timeToLiveWithSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var response = await responseCacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = result;
                return;
            }

            var excutedActionContext = await next.Invoke();

            if (excutedActionContext.Result is OkObjectResult ok && ok.Value is not null)
                await responseCacheService.CacheResponseAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(_timeToLiveWithSeconds));
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach (var (key, value) in request.Query.OrderBy(X => X.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}