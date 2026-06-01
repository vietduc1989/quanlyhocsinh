using System;

namespace ONENET.Application.Common.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) {}
    }
}
