using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Mapping;
using Talabat.Core.Application.Services;

namespace Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
         
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            services.AddScoped(typeof(Func<IBasketService>), serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<IBasketService>();
            });

            return services;
        }
    }
}
