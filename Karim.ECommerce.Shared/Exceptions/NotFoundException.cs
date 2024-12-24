namespace Karim.ECommerce.Shared.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} With ({key}) You Are Try To Get Is Not Found")
        {
            
        }
    }
}
