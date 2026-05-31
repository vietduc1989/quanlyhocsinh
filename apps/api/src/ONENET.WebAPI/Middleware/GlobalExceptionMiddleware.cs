// QUAN-20260530-2301
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.WebAPI.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = ApiResponse.Error("Đã xảy ra lỗi không mong muốn.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = ApiResponse.Error(
                        "Validation Error",
                        new System.Collections.Generic.List<ApiError>(
                            validationException.Errors.Select(e => new ApiError(e.Key, e.Value.First()))
                        ));
                    _logger.LogWarning(validationException, "Validation failed: {Message}", validationException.Message);
                    break;
                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response = ApiResponse.Error(notFoundException.Message);
                    _logger.LogWarning(notFoundException, "Resource not found: {Message}", notFoundException.Message);
                    break;
                case ConflictException conflictException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    response = ApiResponse.Error(
                        conflictException.Message,
                        conflictException.ErrorCode ?? "CONFLICT",
                        conflictException.Message,
                        conflictException.ConflictDetails
                    );
                    _logger.LogWarning(conflictException, "Conflict occurred: {Message}", conflictException.Message);
                    break;
                // Add more specific exceptions here (e.g., ForbiddenException, UnauthorizedException)
                default:
                    _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
                    // For generic 500 errors, avoid exposing sensitive details.
                    response = ApiResponse.Error("Đã xảy ra lỗi nội bộ. Vui lòng thử lại sau.");
                    break;
            }

            var result = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await context.Response.WriteAsync(result);
        }
    }
}