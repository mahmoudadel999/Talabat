using System.Net;
using System.Text.Json;
using Talabat.APIs.Controllers.Errors;
using Talabat.Core.Application.Exceptions;

namespace Talabat.APIs.Middlewares
{
    // Convension base
    public class ExceptionHandlerMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ExceptionHandlerMiddlewares> _logger;

        public ExceptionHandlerMiddlewares(
            RequestDelegate next,
            IWebHostEnvironment webHostEnvironment,
            ILogger<ExceptionHandlerMiddlewares> logger
            )
        {
            _next = next;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;
            switch (ex)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application.json";

                    response = new ApiResponse(404, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case ValidationException validationException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application.json";

                    response = new ApiValidationResponse(ex.Message)
                    {
                        Errors = validationException.Errors
                    };
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application.json";

                    response = new ApiResponse(400, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case UnauthorizedAccessException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application.json";

                    response = new ApiResponse(401, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                default:
                    response = _webHostEnvironment.IsDevelopment()
                        ? response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, details: ex.StackTrace?.ToString())
                        : response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application.json";

                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response.ToString()));
                    break;
            }
        }
    }
}
