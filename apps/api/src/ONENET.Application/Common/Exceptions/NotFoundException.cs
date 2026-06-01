using System;
using System.Net;

namespace ONENET.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
        public string ErrorMessage { get; }

        public NotFoundException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
            ErrorMessage = $"Entity \"{name}\" ({key}) was not found.";
        }
    }
}