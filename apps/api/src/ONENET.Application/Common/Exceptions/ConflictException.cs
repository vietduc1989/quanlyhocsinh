<!-- QUAN-20260530-2301 -->
using System;
using System.Net;

namespace ONENET.Application.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public string ErrorCode { get; }
        public string ErrorMessage { get; }

        public ConflictException(string message, string errorCode = "CONFLICT") : base(message)
        {
            ErrorCode = errorCode;
            ErrorMessage = message;
        }
    }
}