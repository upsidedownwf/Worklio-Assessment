using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WorklioAssessment.Helpers
{
    public static class WorklioAssessmentExtensionMethods
    {

        internal static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var logger = serviceProvider.GetRequiredService<IMyLogger>();
            var emailer = serviceProvider.GetService<IEmailer>();
            app.UseExceptionHandler(handler =>
            {
                handler.Run(async (context) =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    string innerexception = string.Empty;
                    if (exception == null) return;
                    try
                    {
                        if (exception.InnerException != null)
                        {
                            innerexception = $"\nInner Exception: \n Message: {exception.InnerException.Message}" +
                        $"Source: {exception.InnerException.Source}\n StackTrace -> {exception.InnerException.StackTrace}";
                        }
                        string message = $"Message: {exception.Message}\n Source: {exception.Source}\n StackTrace -> {exception.StackTrace} {innerexception}\n";
                        await emailer.SendMail("Error from WorklioAssessment App", message);
                        await logger.LogMessage(LogLevel.Error, message);
                    }
                    finally
                    {
                        throw exception;
                    }
                });
            });
        }
    }
}
