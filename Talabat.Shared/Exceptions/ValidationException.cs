namespace Talabat.Shared.Exceptions
{
    public class ValidationException : BadRequestException
    {
        public required  IEnumerable<string> Errors { get; set; }
        public ValidationException(string message= "Bad request") : base(message)
        {

        }
    }
}
