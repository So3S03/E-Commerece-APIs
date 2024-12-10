namespace Karim.ECommerce.Shared.Exceptions
{
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException() : base("Forbidden, You Are Not Allow To Excute This Action")
        {
            
        }
    }
}
