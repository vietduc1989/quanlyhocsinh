// QUAN-20260531-154643
using System.Text.Json.Serialization;

namespace ONENET.WebAPI.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<ApiError>? Errors { get; set; }

        public static ApiResponse<T> Success(T data, string? message = null)
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message };
        }

        public static ApiResponse<T> Success(string? message = null)
        {
            return new ApiResponse<T> { Success = true, Message = message };
        }

        public static ApiResponse<T> Failure(string message, List<ApiError>? errors = null)
        {
            return new ApiResponse<T> { Success = false, Message = message, Errors = errors };
        }
    }

    public class ApiError
    {
        public string Field { get; set; } = default!;
        public string Message { get; set; } = default!;
    }

    // Overload for non-generic ApiResponse when no data is expected, or for general status
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<ApiError>? Errors { get; set; }

        public static ApiResponse Success(string? message = null)
        {
            return new ApiResponse { Success = true, Message = message };
        }

        public static ApiResponse Failure(string message, List<ApiError>? errors = null)
        {
            return new ApiResponse { Success = false, Message = message, Errors = errors };
        }
    }
}