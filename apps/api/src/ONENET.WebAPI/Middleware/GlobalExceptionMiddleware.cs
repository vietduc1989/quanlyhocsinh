// QUAN-20260530-2302
// Assume GlobalExceptionMiddleware.cs already exists, modifying to handle new exceptions.
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.WebAPI.Models;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Net;
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
            var httpStatusCode = HttpStatusCode.InternalServerError;
            var response = new ApiResponse { Success = false };

            using (LogContext.PushProperty("ExceptionType", exception.GetType().Name))
            using (LogContext.PushProperty("ErrorMessage", exception.Message))
            {
                switch (exception)
                {
                    case ValidationException validationException:
                        httpStatusCode = HttpStatusCode.BadRequest;
                        response.Message = "Validation Error";
                        response.Errors = new List<ApiError>();
                        foreach (var error in validationException.Errors)
                        {
                            response.Errors.Add(new ApiError { Field = error.PropertyName, Message = error.ErrorMessage });
                        }
                        _logger.LogWarning(validationException, "Validation exception occurred for request {Path}", context.Request.Path);
                        break;
                    case NotFoundException notFoundException:
                        httpStatusCode = HttpStatusCode.NotFound;
                        response.Message = notFoundException.Message;
                        _logger.LogWarning(notFoundException, "Not Found exception occurred for request {Path}", context.Request.Path);
                        break;
                    case BusinessRuleException businessRuleException:
                        httpStatusCode = HttpStatusCode.Conflict; // 409 Conflict for business rule violations
                        response.Message = businessRuleException.Message;
                        _logger.LogWarning(businessRuleException, "Business Rule exception occurred for request {Path}", context.Request.Path);
                        break;
                    case UnauthorizedAccessException:
                        httpStatusCode = HttpStatusCode.Forbidden; // 403 Forbidden
                        response.Message = "Access Denied.";
                        _logger.LogWarning(exception, "Unauthorized access attempt for request {Path}", context.Request.Path);
                        break;
                    case DbUpdateConcurrencyException concurrencyException:
                        httpStatusCode = HttpStatusCode.Conflict;
                        response.Message = "Dữ liệu đã được cập nhật bởi người dùng khác. Vui lòng thử lại.";
                        _logger.LogWarning(concurrencyException, "Concurrency conflict detected for request {Path}", context.Request.Path);
                        break;
                    default:
                        response.Message = "An unexpected error occurred.";
                        _logger.LogError(exception, "Unhandled exception occurred for request {Path}", context.Request.Path);
                        break;
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }
}