using System.Text.Json;

namespace Talabat.APIs.Controllers.Errors
{
    public class ApiResponse(int statusCode, string? message = null)
    {
        public int StatusCode { get; set; } = statusCode;
        public string? ErrorMessage { get; set; } = message ?? GetDefaultMessageForStatusCode(statusCode);

        private static string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, You have made",
                401 => "Authorized, You are not",
                404 => "Resource wasn't found",
                500 => "Server error",
                _ => null
            };
        }

        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
