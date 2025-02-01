using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Controllers.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Middlewares;
using Talabat.APIs.Services;
using Talabat.Core.Application;
using Talabat.Core.Application.Abstraction;
using Talabat.Infrastructure;
using Talabat.Infrastructure.Persistence;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure services

            // Add services to the container.

            builder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                    options.InvalidModelStateResponseFactory = (action) =>
                    {
                        {
                            var errors = action.ModelState
                                .Where(P => P.Value!.Errors.Count > 0)
                                .SelectMany(P => P.Value!.Errors)
                                .Select(E => E.ErrorMessage);

                            return new BadRequestObjectResult(new ApiValidationResponse()
                            {
                                Errors = errors
                            });
                        }

                    };
                })
                .AddApplicationPart(typeof(Controllers.AssemblyInfo).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

            // Configuring connection string
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddScoped(typeof(ILoginUserService), typeof(LoginUserService));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddIdentityService(builder.Configuration);


            #endregion

            var app = builder.Build();

            #region Database initialization

            await app.InitializeDbContextAsync();

            #endregion

            #region Configure middlewares

            app.UseMiddleware<ExceptionHandlerMiddlewares>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
