using Entities.Exceptions;
using MarketplaceCrypto.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Service.Contracts;

namespace CryptoMarketplace.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                int userId = 0;
                string email = "";
                string requestUrl = context.Request.GetEncodedUrl();
                var ip = context.Connection.RemoteIpAddress;
                string clientIp = ip == null ? "" : ip.ToString();

                if (context.User.Claims.Any())
                {
                    email = context.User.Claims.First(c => c.Type == "Email").Value;
                    userId = Convert.ToInt32(context.User.Claims.First(c => c.Type == "Id").Value);
                }

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    string errorMessage = $"UserId: {userId}, User Email: {email}, ClientIP: {clientIp}, Request URL: {requestUrl} => Error details: {contextFeature.Error}";
                    logger.LogError(errorMessage);

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = context.Response.StatusCode == StatusCodes.Status404NotFound || context.Response.StatusCode == StatusCodes.Status400BadRequest
                            ? contextFeature.Error.Message : "Something went wrong, please try again later!",

                    }.ToString());
                }
            });
        });
    }
}
