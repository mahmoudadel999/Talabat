using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Talabat.Core.Domain.Contract.Infrastructure;
using Talabat.Infrastructure.Basket_Repository;

namespace Talabat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.AddSingleton(typeof(IConnectionMultiplexer), provider =>
            {
                var connectionString = configuration["ConnectionStrings:Redis"];
                var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString!);
                return connectionMultiplexer;
            });

            return services;
        }
    }
}
