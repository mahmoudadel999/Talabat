namespace Talabat.APIs.Controllers.Errors
{
    public class ApiValidationResponse : ApiResponse
    {
        public required IEnumerable<string> Errors { get; set; }
        public ApiValidationResponse(string? message = null) : base(400, message)
        {
        }

        
    }
}
