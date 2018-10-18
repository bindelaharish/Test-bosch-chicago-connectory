using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RB.JobAssistant.Core
{
    public class ExceptionMessage
    {
        public Guid ErrorId { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public static class ExceptionConfig
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var feature = context.Features.Get<IExceptionHandlerFeature>();

                    if (feature != null)
                    {
                        var errorId = Guid.NewGuid();
                        logger.LogError(feature.Error, $"Error Id: {errorId}");

                        var error = new JsonResult(new ExceptionMessage()
                        {
                            ErrorId = errorId,
                            StatusCode = context.Response.StatusCode,
                            Message = $"An internal server error occured. Please contact the support team and refer error id as {errorId}"
                        }).Value;

                        using (var writer = new StreamWriter(context.Response.Body))
                        {
                            new JsonSerializer().Serialize(writer, error);
                            await writer.FlushAsync().ConfigureAwait(false);
                        }
                    }
                });
            });
        }
    }
}