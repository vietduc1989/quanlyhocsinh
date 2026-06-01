// QUAN-20260531-154643
using System.Net;

namespace ONENET.Application.Common.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message = "Resource not found.")
            : base(message, null, HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.", null, HttpStatusCode.NotFound)
        {
        }
    }
}