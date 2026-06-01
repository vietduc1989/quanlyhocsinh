// QUAN-20260531-154643
using System.Net;
using System.Text.Json;
using ONENET.Application.Common.Exceptions;
using ONENET.WebAPI.Common;
using FluentValidation; // Import FluentValidation's ValidationException

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
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            var apiResponse = ApiResponse.Failure("An unexpected error occurred.");
            var statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    var validationErrors = validationException.Errors
                        .Select(error => new ApiError { Field = error.PropertyName, Message = error.ErrorMessage })
                        .ToList();
                    apiResponse = ApiResponse.Failure("Một hoặc nhiều lỗi xác thực đã xảy ra.", validationErrors);
                    _logger.LogWarning(exception, "Validation error occurred: {Message}", exception.Message);
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    apiResponse = ApiResponse.Failure(notFoundException.Message);
                    _logger.LogWarning(exception, "Not Found error occurred: {Message}", exception.Message);
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    apiResponse = ApiResponse.Failure("Authentication failed.");
                    _logger.LogWarning(exception, "Unauthorized access: {Message}", exception.Message);
                    break;
                case ForbiddenException: // Assuming a custom ForbiddenException exists in Application.Common.Exceptions
                    statusCode = HttpStatusCode.Forbidden;
                    apiResponse = ApiResponse.Failure("You do not have permission to perform this action.");
                    _logger.LogWarning(exception, "Forbidden access: {Message}", exception.Message);
                    break;
                default:
                    // Log critical errors for unhandled exceptions
                    _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
                    apiResponse = ApiResponse.Failure("Đã có lỗi xảy ra, vui lòng thử lại sau.");
                    break;
            }

            response.StatusCode = (int)statusCode;
            await response.WriteAsync(JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }
}