using System.Net;

namespace ONENET.Application.Common.Exceptions
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message = "You do not have permission to perform this action.")
            : base(message)
        {
        }
    }
}
