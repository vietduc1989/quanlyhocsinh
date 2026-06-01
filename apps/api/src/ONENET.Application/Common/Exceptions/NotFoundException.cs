// QUAN-20260530-2302
// Assume NotFoundException.cs already exists, if not, create it.
using System;
`r`n// QUAN-20260531-154643
using System.Net;
`r`n>>>>>>> origin/dev
namespace ONENET.Application.Common.Exceptions
{
    public class NotFoundException : CustomException
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
        public NotFoundException(string message = "Resource not found.")
            : base(message, null, HttpStatusCode.NotFound)
        {
        }
=======
>>>>>>> origin/dev
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.", null, HttpStatusCode.NotFound)
        {
        }
    }
}