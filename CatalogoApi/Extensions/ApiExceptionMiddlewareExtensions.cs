using CatalogoApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CatalogoApi.Extensions
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigueExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contentFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contentFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contentFeature.Error.Message,
                            Trace = contentFeature.Error.StackTrace,
                        }.ToString());
                    }
                });
            });
        }
    }
}
