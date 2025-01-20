using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
         
            services.AddDbContext<StoreDbContext>(options =>
              {
                  options
                  .UseLazyLoadingProxies()
                  .UseSqlServer(configuration.GetConnectionString("StoreContext"));
              });


            services.AddScoped(typeof(IStoreDbContextInitializer), typeof(StoreDbContextInitializer));
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

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
