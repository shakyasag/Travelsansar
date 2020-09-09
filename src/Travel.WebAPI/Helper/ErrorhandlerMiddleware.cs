using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Application.Common.Error;

namespace Travel.WebAPI.Helper
{
    public class ErrorhandlerMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorhandlerMiddleware> _logger;
         public ErrorhandlerMiddleware(RequestDelegate next, ILogger<ErrorhandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorhandlerMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;
            object errors = null;
            switch (ex)
            {
                case RestException re:
                    logger.LogError(ex, "REST ERROR");
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case ValidateException ValidateException:
                    code = HttpStatusCode.BadRequest;
                    errors = JsonSerializer.Serialize(ValidateException.Failures);
                    break;

                case Exception e:
                    logger.LogError(ex, "SERVER ERROR");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)code;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (errors != null)
            {
                var result = JsonSerializer.Serialize(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }

        
    }



    public static class ErrorhandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorhandlerMiddleware>();
        }
    }
}
