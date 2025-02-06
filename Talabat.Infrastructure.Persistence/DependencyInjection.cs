using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Talabat.Core.Domain.Contract.Persistence;
using Talabat.Core.Domain.Contract.Persistence.DbContextInitializer;
using Talabat.Infrastructure.Persistence.Data;
using Talabat.Infrastructure.Persistence.Data.Interceptors;
using Talabat.Infrastructure.Persistence.Identity;

namespace Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region StoreDbContext

            services.AddDbContext<StoreDbContext>((serviceProvider, options) =>
              {
                  options
                  .UseLazyLoadingProxies()
                  .UseSqlServer(configuration.GetConnectionString("StoreContext"))
                  .AddInterceptors(serviceProvider.GetRequiredService<AuditInterceptor>());
              });
            services.AddScoped(typeof(AuditInterceptor));

            services.AddScoped(typeof(IStoreDbContextInitializer), typeof(StoreDbContextInitializer));

            #endregion

            #region StoreIdentityDbContext

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreIdentityContext"));
            });


            services.AddScoped(typeof(IStoreIdentityDbContextInitializer), typeof(StoreIdentityDbContextInitializer));

            #endregion

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            return services;
        }
    }
}
