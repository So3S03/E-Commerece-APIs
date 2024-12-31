using Karim.ECommerce.APIs.Controllers.Errors;
using Karim.ECommerce.Shared.Exceptions;
using System.Net;

namespace Karim.ECommerce.APIs.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);

                if(context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    var Response = new ErrorResponse((int)HttpStatusCode.NotFound, $"The Requested End Point: {context.Request.Path.Value} is Not Found");
                    await context.Response.WriteAsync(Response.ToString());
                }
                else if(context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    var Response = new ErrorResponse((int)HttpStatusCode.Unauthorized);
                    await context.Response.WriteAsync(Response.ToString());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                ErrorExceptionResponse Response;

                if (environment.IsDevelopment())
                    switch (ex)
                    {
                        case NotFoundException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.NotFound, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        case BadRequestException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.BadRequest, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case UauthorizedException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.Unauthorized, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            break;
                        case ForbiddenException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.Forbidden, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            break;
                        case ConflictException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.Conflict, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                            break;
                        case NotImplimentedException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.NotImplemented, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                            break;
                        case BadGetwayException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.BadGateway, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                            break;
                        case ServiceUnavailableException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.ServiceUnavailable, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            break;
                        case GatewayTimeoutException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.GatewayTimeout, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                            break;
                        default:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.InternalServerError, ex.StackTrace?.ToString(), ex.Message);
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }
                else
                    switch (ex)
                    {
                        case NotFoundException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.NotFound);
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        case BadRequestException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.BadRequest);
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case UauthorizedException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.Unauthorized);
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            break;
                        case ForbiddenException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.Forbidden);
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            break;
                        case ConflictException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.Conflict);
                            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                            break;
                        case NotImplimentedException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.NotImplemented);
                            context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                            break;
                        case BadGetwayException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.BadGateway);
                            context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                            break;
                        case ServiceUnavailableException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.ServiceUnavailable);
                            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            break;
                        case GatewayTimeoutException:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.GatewayTimeout);
                            context.Response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                            break;
                        default:
                            Response = new ErrorExceptionResponse((int)HttpStatusCode.InternalServerError);
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    };

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(Response.ToString());
            }

        }

    }
}
