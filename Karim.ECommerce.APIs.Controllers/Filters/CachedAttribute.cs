using Karim.ECommerce.Domain.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Karim.ECommerce.APIs.Controllers.Filters
{
    internal class CachedAttribute(int timeToLiveInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var Response = await CacheService.GetCahedResponseAsync(CacheKey);
            if (!string.IsNullOrEmpty(Response))
            {
                context.Result = new ContentResult()
                {
                    Content = Response,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }

            var ExecutedAction = await next.Invoke();
            if(ExecutedAction.Result is OkObjectResult okObjResult && okObjResult.Value is not null)
            {
                await CacheService.CacheTheResponseAsync(CacheKey, okObjResult.Value, TimeSpan.FromSeconds(timeToLiveInSec));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach(var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
