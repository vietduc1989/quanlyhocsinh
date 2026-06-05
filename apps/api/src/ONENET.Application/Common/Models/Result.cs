// QUAN-20260604-153038
using System.Collections.Generic;
using System.Linq;

namespace ONENET.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public List<string> Errors { get; private set; } = new List<string>();
        public string Message => Errors.Any() ? string.Join(" | ", Errors) : "Operation successful.";

        protected Result(bool isSuccess, List<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success() => new(true, new List<string>());
        public static Result Failure(string error) => new(false, new List<string> { error });
        public static Result Failure(List<string> errors) => new(false, errors);

        public void AddError(string error)
        {
            if (IsSuccess)
            {
                IsSuccess = false;
            }
            Errors.Add(error);
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; private set; }

        private Result(bool isSuccess, T? value, List<string> errors)
            : base(isSuccess, errors)
        {
            Value = value;
        }

        public new static Result<T> Success(T value) => new(true, value, new List<string>());
        public new static Result<T> Failure(string error) => new(false, default, new List<string> { error });
        public new static Result<T> Failure(List<string> errors) => new(false, default, errors);

        // Allow implicit conversion from Result to Result<T> for chained failures
        public static implicit operator Result<T>(Result result)
        {
            return new Result<T>(result.IsSuccess, default, result.Errors);
        }
    }
}