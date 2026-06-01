// QUAN-20260531-154643
using System.Net;

namespace ONENET.Application.Common.Exceptions
{
    public class ConflictException : CustomException
    {
        public ConflictException(string message)
            : base(message, null, HttpStatusCode.Conflict)
        {
        }
    }

    // Assuming CustomException exists based on Error Handling & Logging guideline
    // If not, a simple base exception should be created.
    public abstract class CustomException : Exception
    {
        public List<string> ErrorMessages { get; }
        public HttpStatusCode StatusCode { get; }

        protected CustomException(string message, List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors ?? new List<string>();
            StatusCode = statusCode;
        }
    }
}