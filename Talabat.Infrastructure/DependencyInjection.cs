using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using Talabat.Infrastructure.Basket_Repository;
using Talabat.Infrastructure.Cache_Service;
using Talabat.Infrastructure.Payment_Service;
using Talabat.Shared.Models;

namespace Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(typeof(IConnectionMultiplexer), (_) =>
            {
                var connectionString = configuration["ConnectionStrings:Redis"];
                var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString!);
                return connectionMultiplexer;
            });

            services.AddSingleton(typeof(IResponseCacheService), typeof(ResponseCacheService));

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

            return services;
        }
    }
}
