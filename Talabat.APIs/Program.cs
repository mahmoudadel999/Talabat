using Talabat.APIs.Extensions;
using Talabat.APIs.Services;
using Talabat.Core.Application.Abstraction;
using Talabat.Infrastructure.Persistence;
using Talabat.Core.Application;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            #region Configure services

            // Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Controllers.AssemblyInfo).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuring connection string

            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddScoped(typeof(ILoginUserService), typeof(LoginUserService));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddApplicationServices();

            #endregion

            var app = builder.Build();

            #region Database initialization

            await app.InitializeStoreDbContextAsync();

            #endregion

            #region Configure middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
