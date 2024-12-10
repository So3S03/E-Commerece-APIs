namespace Karim.ECommerce.Domain._Common.Initializer
{
    public  interface IDbInitializer
    {
        Task DbInitializeAsync();
        Task SeedAsync();
    }
}
