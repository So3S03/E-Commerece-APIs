using System.Runtime.CompilerServices;

namespace Karim.ECommerce.APIs.Controllers.Errors
{
    public class ErrorValidationResponse : ErrorResponse
    {
        public IEnumerable<ValidationError> Errors { get; set; }
        public ErrorValidationResponse(IEnumerable<ValidationError> errors, string? message = null) : base(400, message)
        {
            Errors = errors;
        }

        public class ValidationError
        {
            public required string Field { get; set; }
            public required IEnumerable<string> Errors { get; set; }
        }
    }
}
