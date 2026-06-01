// QUAN-20260530-2301
namespace ONENET.Application.Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException()
        : base()
    {
    }

    public ConflictException(string message)
        : base(message)
    {
    }

    public ConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}