
using Karim.ECommerce.APIs.Controllers;
using Karim.ECommerce.APIs.Controllers.Errors;
using Karim.ECommerce.APIs.Extensions;
using Karim.ECommerce.APIs.Middlewares;
using Karim.ECommerce.APIs.Services;
using Karim.ECommerce.Application;
using Karim.ECommerce.Domain.Contracts;
using Karim.ECommerce.Infrastructure.Persistence;
using Karim.ECommerce.Shared.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Karim.ECommerce.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region DI Container
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = false;
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var Errors = actionContext.ModelState.Where(E => E.Value!.Errors.Count > 0)
                                .Select(E => new ErrorValidationResponse.ValidationError() { Field = E.Key, Errors = E.Value!.Errors.Select(E => E.ErrorMessage) });

                        return new BadRequestObjectResult( new ErrorValidationResponse(Errors) );
                    };
                })
                .AddJsonOptions( opt =>
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                })
                .AddApplicationPart(typeof(ControllersAssembly).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
            #endregion

            var app = builder.Build();
            await app.InitializeAsync<IStoreDbInitializer>();

            // Configure the HTTP request pipeline.
            #region Configure Kistrell Services
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
