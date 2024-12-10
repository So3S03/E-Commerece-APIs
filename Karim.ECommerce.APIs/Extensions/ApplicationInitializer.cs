using Karim.ECommerce.Domain._Common.Initializer;

namespace Karim.ECommerce.APIs.Extensions
{
    public static class ApplicationInitializer
    {
        public async static Task InitializeAsync<T>(this IApplicationBuilder app)
            where T : IDbInitializer
        {
            using var scope = app.ApplicationServices.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var DbInitializer = services.GetRequiredService<T>();

            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await DbInitializer.DbInitializeAsync();
                await DbInitializer.SeedAsync();
            }
            catch (Exception ex)
            {

                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, ex.Message);
            }
        }
    }
}
