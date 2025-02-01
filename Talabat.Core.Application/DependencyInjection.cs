using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Orders;
using Talabat.Core.Application.Mapping;
using Talabat.Core.Application.Services;
using Talabat.Core.Application.Services.Basket;
using Talabat.Core.Application.Services.Orders;

namespace Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));


            services.AddScoped(typeof(IBasketService), typeof(BasketService));

            services.AddScoped(typeof(Func<IBasketService>), serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IBasketService>();
            });

            services.AddScoped(typeof(IOrderService), typeof(OrderService));

            services.AddScoped(typeof(Func<IOrderService>), serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IOrderService>();
            });

            return services;
        }
    }
}
