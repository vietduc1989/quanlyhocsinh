using System.Collections.Generic;

namespace ONENET.WebAPI.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<ApiError> Errors { get; set; } = new List<ApiError>();

        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, List<ApiError>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = errors ?? new List<ApiError>()
            };
        }

        public static ApiResponse<T> ValidationFailureResponse(IDictionary<string, string[]> validationErrors, string message = "Validation failed")
        {
            var apiErrors = new List<ApiError>();
            foreach (var error in validationErrors)
            {
                foreach (var msg in error.Value)
                {
                    apiErrors.Add(new ApiError { Field = error.Key, Message = msg });
                }
            }
            return ErrorResponse(message, apiErrors);
        }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public List<ApiError> Errors { get; set; } = new List<ApiError>();

        public static ApiResponse SuccessResponse(object? data = null, string? message = null)
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse ErrorResponse(string message, List<ApiError>? errors = null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Data = null,
                Errors = errors ?? new List<ApiError>()
            };
        }

        public static ApiResponse ValidationFailureResponse(IDictionary<string, string[]> validationErrors, string message = "Validation failed")
        {
            var apiErrors = new List<ApiError>();
            foreach (var error in validationErrors)
            {
                foreach (var msg in error.Value)
                {
                    apiErrors.Add(new ApiError { Field = error.Key, Message = msg });
                }
            }
            return ErrorResponse(message, apiErrors);
        }
    }

    public class ApiError
    {
        public string? Field { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Code { get; set; } // Optional: for custom error codes
    }
}