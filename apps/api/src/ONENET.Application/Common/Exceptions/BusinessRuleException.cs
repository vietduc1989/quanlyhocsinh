// Assume BusinessRuleException.cs already exists, if not, create it.
using System;

namespace ONENET.Application.Common.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException() : base() { }

        public BusinessRuleException(string message) : base(message) { }

        public BusinessRuleException(string message, Exception innerException) : base(message, innerException) { }
    }
}
