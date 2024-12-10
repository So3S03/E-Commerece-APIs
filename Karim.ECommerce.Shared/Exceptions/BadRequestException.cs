namespace Karim.ECommerce.Shared.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message = "Bad Request") : base(message)
        {
            
        }
    }
}
