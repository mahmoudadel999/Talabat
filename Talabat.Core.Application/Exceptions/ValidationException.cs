namespace Talabat.Core.Application.Exceptions
{
    public class ValidationException : BadRequestException
    {
        public required  IEnumerable<string> Errors { get; set; }
        public ValidationException(string message= "Bad request") : base(message)
        {

        }
    }
}
