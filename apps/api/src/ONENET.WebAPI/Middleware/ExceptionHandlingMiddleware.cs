// QUAN-20260604-153038
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ONENET.WebAPI.Models;
using Serilog.Context;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ONENET.WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Structured Logging
                using (LogContext.PushProperty("ExceptionType", ex.GetType().Name))
                using (LogContext.PushProperty("StackTrace", ex.StackTrace))
                using (LogContext.PushProperty("RequestPath", httpContext.Request.Path))
                using (LogContext.PushProperty("User", httpContext.User?.Identity?.Name ?? "Anonymous"))
                {
                    _logger.LogError(ex, "An unhandled exception occurred during request processing.");
                }

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = ApiResponse<object>.Failure("Đã có lỗi xảy ra trong quá trình xử lý yêu cầu. Vui lòng thử lại sau.", "Internal Server Error");

            // Optionally, for specific exception types, you can return different status codes and messages
            // e.g., if (exception is UnauthorizedAccessException) context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}