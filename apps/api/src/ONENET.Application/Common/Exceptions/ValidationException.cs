using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentValidation.Results;

namespace ONENET.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}