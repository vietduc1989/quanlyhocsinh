// QUAN-20260531-154643
using System.Net;
using FluentValidation;
using ONENET.Application.Common.Exceptions;
using ONENET.WebAPI.Common; // For ApiResponse
using Serilog; // For structured logging

namespace ONENET.WebAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger; // Use ILogger from Serilog

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
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";
            List<ApiError>? errors = null;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Validation failed.";
                    errors = validationException.Errors
                        .Select(e => new ApiError { Field = e.PropertyName, Message = e.ErrorMessage })
                        .ToList();
                    _logger.Warning(validationException, "Validation error occurred for request path {Path}.", context.Request.Path);
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    _logger.Warning(notFoundException, "Not Found error occurred for request path {Path}.", context.Request.Path);
                    break;
                case ForbiddenException forbiddenException:
                    statusCode = HttpStatusCode.Forbidden;
                    message = forbiddenException.Message;
                    _logger.Warning(forbiddenException, "Forbidden error occurred for request path {Path}. User might lack permission.", context.Request.Path);
                    break;
                case ConflictException conflictException:
                    statusCode = HttpStatusCode.Conflict;
                    message = conflictException.Message;
                    _logger.Warning(conflictException, "Conflict error occurred for request path {Path}. Resource already exists or cannot be modified.", context.Request.Path);
                    break;
                case CustomException customException: // Catch other custom exceptions if they inherit CustomException
                    statusCode = customException.StatusCode;
                    message = customException.Message;
                    errors = customException.ErrorMessages.Select(e => new ApiError { Message = e }).ToList();
                    _logger.Error(customException, "Custom application exception occurred: {Message}", customException.Message);
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected internal server error occurred.";
                    _logger.Error(exception, "An unhandled exception occurred during request processing for path {Path}.", context.Request.Path);
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            var response = ApiResponse.Error(message, (int)statusCode, errors);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}