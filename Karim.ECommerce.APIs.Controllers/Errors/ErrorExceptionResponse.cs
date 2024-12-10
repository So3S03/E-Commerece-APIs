using System.Text.Json;

namespace Karim.ECommerce.APIs.Controllers.Errors
{
    public class ErrorExceptionResponse : ErrorResponse
    {
        public string? Details { get; set; }
        public ErrorExceptionResponse(int statusCode, string? details = null, string? message = null) : base(statusCode, message)
        {
            Details = details;
        }

        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
    }
}
