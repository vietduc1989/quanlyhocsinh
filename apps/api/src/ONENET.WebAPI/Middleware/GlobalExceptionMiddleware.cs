using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.WebAPI.Common;

namespace ONENET.WebAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception occurred during request processing: {RequestPath}", httpContext.Request.Path);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = ApiResponse.ErrorResponse("An unexpected error occurred.");

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = (int)validationException.StatusCode; // 400
                    response = ApiResponse.ValidationFailureResponse(validationException.Errors, "Validation failed.");
                    _logger.LogWarning("Validation exception: {ExceptionMessage} {@ValidationErrors}", validationException.Message, validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)notFoundException.StatusCode; // 404
                    response = ApiResponse.ErrorResponse(notFoundException.ErrorMessage);
                    _logger.LogWarning("Not found exception: {ExceptionMessage}", notFoundException.Message);
                    break;
                case ConflictException conflictException:
                    context.Response.StatusCode = (int)conflictException.StatusCode; // 409
                    response = ApiResponse.ErrorResponse(conflictException.ErrorMessage, new List<ApiError> { new ApiError { Code = conflictException.ErrorCode, Message = conflictException.ErrorMessage } });
                    _logger.LogWarning("Conflict exception: {ExceptionMessage}", conflictException.Message);
                    break;
                // Add more custom exceptions here (e.g., ForbiddenException for 403)
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response = ApiResponse.ErrorResponse("An unexpected internal server error occurred.");
                    // Log the full exception for 500 errors.
                    _logger.LogError(exception, "Unhandled exception: {ExceptionMessage}", exception.Message);
                    break;
            }

            var result = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await context.Response.WriteAsync(result);
        }
    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
