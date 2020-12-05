
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;

namespace Common.Web.Middleware
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.logger = logger;
            this.next = next;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(exception);

            return context.Response.WriteAsync(new ErrorDetails()
            {

                Message = exception.Message,
                StackTrace = env.IsDevelopment() ? exception.StackTrace : string.Empty
            }.ToString());
        }

        private int GetStatusCode(Exception exception)
        {
            switch (exception.GetType().Name)
            {
                case nameof(NotFoundException):
                    return 404;
                case nameof(AccessDeniedException):
                    return 401;
                default:
                    return 500;

            }

        }

        class ErrorDetails
        {
            public string Message { get; set; }
            public string StackTrace { get; set; }

            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
    }




}
