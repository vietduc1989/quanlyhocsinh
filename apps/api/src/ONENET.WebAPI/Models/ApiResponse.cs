// QUAN-20260530-2302
// Assume ApiResponse.cs already exists, creating a basic version if not.
using System.Collections.Generic;

namespace ONENET.WebAPI.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public object? Data { get; set; }
        public string? Message { get; set; }
        public List<ApiError>? Errors { get; set; }

        public static ApiResponse Success(object? data = null, string? message = null)
        {
            return new ApiResponse { Success = true, Data = data, Message = message, Errors = null };
        }

        public static ApiResponse Failure(string message, List<ApiError>? errors = null)
        {
            return new ApiResponse { Success = false, Data = null, Message = message, Errors = errors };
        }

        public static ApiResponse ValidationFailure(List<ApiError> errors, string message = "Validation failed")
        {
            return new ApiResponse { Success = false, Data = null, Message = message, Errors = errors };
        }
    }

    public class ApiError
    {
        public string? Field { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}