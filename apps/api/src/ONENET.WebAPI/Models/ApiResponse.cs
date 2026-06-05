// QUAN-20260604-153038
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ONENET.WebAPI.Models
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string Message { get; private set; }
        public List<string> Errors { get; private set; }

        private ApiResponse(bool success, T? data, string message, List<string> errors)
        {
            IsSuccess = success;
            Data = data;
            Message = message;
            Errors = errors;
        }

        public static ApiResponse<T> Success(T? data, string message = "Thao tác thành công.")
        {
            return new ApiResponse<T>(true, data, message, new List<string>());
        }

        public static ApiResponse<T> Failure(string error, string message = "Thao tác thất bại.")
        {
            return new ApiResponse<T>(false, default, message, new List<string> { error });
        }

        public static ApiResponse<T> Failure(List<string> errors, string message = "Thao tác thất bại.")
        {
            return new ApiResponse<T>(false, default, message, errors);
        }
    }
}