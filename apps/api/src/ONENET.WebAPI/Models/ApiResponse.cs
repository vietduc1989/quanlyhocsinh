// QUAN-20260530-2301
using System.Collections.Generic;

namespace ONENET.WebAPI.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public List<ApiError>? Errors { get; set; }

        public static ApiResponse Success(object? data = null, string? message = "Thành công.")
        {
            return new ApiResponse { Success = true, Message = message, Data = data, Errors = null };
        }

        public static ApiResponse Error(string message, List<ApiError>? errors = null)
        {
            return new ApiResponse { Success = false, Message = message, Data = null, Errors = errors };
        }

        public static ApiResponse Error(string message, string field, string errorDescription)
        {
            return new ApiResponse { Success = false, Message = message, Data = null, Errors = new List<ApiError> { new ApiError(field, errorDescription) } };
        }

        public static ApiResponse Error(string message, string errorCode, string errorDescription, object? conflictDetails = null)
        {
            return new ApiResponse { Success = false, Message = message, Data = null, Errors = new List<ApiError> { new ApiError(errorCode, errorDescription, conflictDetails) } };
        }
    }

    public class ApiError
    {
        public string Field { get; set; } // Or Code
        public string Message { get; set; }
        public object? Details { get; set; } // For additional context, e.g., conflict details

        public ApiError(string field, string message, object? details = null)
        {
            Field = field;
            Message = message;
            Details = details;
        }
    }
}