using Karim.ECommerce.APIs.Controllers;
using Karim.ECommerce.APIs.Controllers.Errors;
using Karim.ECommerce.APIs.Extensions;
using Karim.ECommerce.APIs.Middlewares;
using Karim.ECommerce.APIs.Services;
using Karim.ECommerce.Application;
using Karim.ECommerce.Domain.Contracts.Persistence;
using Karim.ECommerce.Domain.Entities.Security;
using Karim.ECommerce.Infrastructure;
using Karim.ECommerce.Infrastructure.Persistence;
using Karim.ECommerce.Infrastructure.Persistence._SecurityDatabase;
using Karim.ECommerce.Shared.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>( identityOptions =>
            {
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;

                identityOptions.User.RequireUniqueEmail = true;

                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(double.Parse(builder.Configuration.GetSection("IdentityOptions")["DefaultLockoutTimeSpanInDays"]!));
                identityOptions.Lockout.MaxFailedAccessAttempts = int.Parse(builder.Configuration.GetSection("IdentityOptions")["MaxFailedAccessAttempts"]!);
                identityOptions.Lockout.AllowedForNewUsers = true;
            } )
                .AddEntityFrameworkStores<SecurityDbContext>();

            builder.Services.AddAuthentication( authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidAudience = builder.Configuration.GetSection("JwtSettings")["Audience"],
                        ValidIssuer = builder.Configuration.GetSection("JwtSettings")["Issure"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings")["SymmetricSecurityKey"]!)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddCors(corsOpt =>
            {
                corsOpt.AddPolicy("ECommercePolicy", policyBuilder =>
                {
                    policyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });
            #endregion

            var app = builder.Build();

            await app.InitializeAsync<IStoreDbInitializer>();
            await app.InitializeAsync<ISecurityDbInitializer>();

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

            app.UseCors("ECommercePolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
