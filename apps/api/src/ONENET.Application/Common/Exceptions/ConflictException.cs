// QUAN-20260530-2301
using System;

namespace ONENET.Application.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public string? ErrorCode { get; }
        public object? ConflictDetails { get; }

        public ConflictException()
            : base() { }

        public ConflictException(string message)
            : base(message) { }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException) { }

        public ConflictException(string message, string errorCode, object? conflictDetails = null)
            : base(message)
        {
            ErrorCode = errorCode;
            ConflictDetails = conflictDetails;
        }
    }
}