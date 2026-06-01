// QUAN-20260530-2301
using FluentValidation;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Models;
using Serilog;
using System.Net;
using System.Text.Json;

namespace ONENET.WebAPI.Middleware;

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
        var apiResponse = new ApiResponse { Success = false };

        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                apiResponse.Message = "Validation Error";
                apiResponse.Errors = validationException.Errors.SelectMany(kv => kv.Value.Select(v => new ApiError { Field = kv.Key, Message = v })).ToList();
                _logger.LogWarning(validationException, "Validation failed: {ValidationErrors}", validationException.Errors);
                break;
            case NotFoundException notFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                apiResponse.Message = notFoundException.Message;
                // If NotFoundException constructor supports ApiError list
                if (notFoundException.InnerException is IEnumerable<ApiError> notFoundErrors)
                {
                    apiResponse.Errors = notFoundErrors.ToList();
                }
                _logger.LogWarning(notFoundException, "Resource not found: {Message}", notFoundException.Message);
                break;
            case ConflictException conflictException:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                apiResponse.Message = conflictException.Message;
                // Assuming ConflictException's InnerException might contain a list of ApiError
                if (conflictException.InnerException is IEnumerable<ApiError> conflictErrors)
                {
                    apiResponse.Errors = conflictErrors.ToList();
                }
                else if (exception.InnerException is not null)
                {
                    apiResponse.Errors = new List<ApiError> { new() { Code = "UNKNOWN_CONFLICT", Message = exception.InnerException.Message } };
                }
                _logger.LogWarning(conflictException, "Conflict occurred: {Message}", conflictException.Message);
                break;
            case UnauthorizedAccessException _: // Not currently used directly but good practice
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                apiResponse.Message = "Unauthorized";
                _logger.LogWarning(exception, "Unauthorized access attempt.");
                break;
            case ForbiddenException _: // Not currently used directly but good practice
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                apiResponse.Message = "Forbidden";
                _logger.LogWarning(exception, "Forbidden access attempt.");
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                apiResponse.Message = "An unexpected error occurred.";
                _logger.LogError(exception, "An unhandled exception occurred: {ErrorMessage}", exception.Message);
                break;
        }

        var jsonResponse = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        await context.Response.WriteAsync(jsonResponse);
    }
}