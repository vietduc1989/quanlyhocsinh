// QUAN-20260530-2301
namespace ONENET.Application.Common.Models;

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<ApiError>? Errors { get; set; }

    public static ApiResponse SuccessResult(string? message = null) => new() { Success = true, Message = message };
    public static ApiResponse<T> SuccessResult<T>(T data, string? message = null) => new() { Success = true, Data = data, Message = message };
    public static ApiResponse ErrorResult(string? message = null, List<ApiError>? errors = null) => new() { Success = false, Message = message, Errors = errors };
    public static ApiResponse<T> ErrorResult<T>(string? message = null, List<ApiError>? errors = null) => new() { Success = false, Message = message, Errors = errors };
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}

public class ApiError
{
    public string? Field { get; set; }
    public string? Message { get; set; }
    public string? Code { get; set; } // For specific error codes like STUDENT_HAS_RELATED_DATA
}