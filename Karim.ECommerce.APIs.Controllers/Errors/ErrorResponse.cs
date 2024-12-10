using System.Text.Json;

namespace Karim.ECommerce.APIs.Controllers.Errors
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        private string? GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "You Have Made A Bad Request",
                401 => "You Are Not Authorized",
                403 => "Forbidden, You Are Not Allow To Excute This Action",
                404 => "Not Found, The Content You Try To Reach Doesn't Exist",
                409 => "Conflict",
                500 => "Internal Server Error",
                501 => "Not Implimented, Some Code You Are Using is Not Fully Implimented",
                502 => "Bad Getway",
                503 => "Service Unavailable",
                504 => "Gateway Timeout",
                _ => null
            };
        }

        public override string ToString() => JsonSerializer.Serialize(this, new JsonSerializerOptions () { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
    }
}
