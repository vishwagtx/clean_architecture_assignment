using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.Exception
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {                   
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var factory = (ILoggerFactory)context.RequestServices.GetService(typeof(ILoggerFactory));
                        var logger = factory.CreateLogger(contextFeature.Error.Source);

                        logger.LogError(contextFeature.Error, $"{contextFeature.Error.StackTrace} | {contextFeature.Error.Message}");
                    }
                });
            });
        }
    }
}
