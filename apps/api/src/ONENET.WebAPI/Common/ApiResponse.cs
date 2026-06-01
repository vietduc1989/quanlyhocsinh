// QUAN-20260531-154643
using System.Net;

namespace ONENET.WebAPI.Common
{
    // Standard API response format as per guideline 6.2
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<ApiError>? Errors { get; set; }

        public static ApiResponse Success(string? message = null)
        {
            return new ApiResponse { Success = true, Message = message };
        }

        public static ApiResponse Error(string? message = "An error occurred.", int statusCode = StatusCodes.Status500InternalServerError, List<ApiError>? errors = null)
        {
            return new ApiResponse { Success = false, Message = message, Errors = errors };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string? message = null)
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message };
        }

        public new static ApiResponse<T> Error(string? message = "An error occurred.", int statusCode = StatusCodes.Status500InternalServerError, List<ApiError>? errors = null)
        {
            return new ApiResponse<T> { Success = false, Data = default, Message = message, Errors = errors };
        }
    }

    public class ApiError
    {
        public string? Field { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}