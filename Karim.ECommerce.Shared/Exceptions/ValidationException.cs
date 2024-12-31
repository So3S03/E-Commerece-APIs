namespace Karim.ECommerce.Shared.Exceptions
{
    public class ValidationException : BadRequestException
    {
        public required IEnumerable<string> Errors { get; set; }
        public ValidationException(string message = "Bad Request, Data Validation Error") : base(message)
        {
            
        }
    }
}
