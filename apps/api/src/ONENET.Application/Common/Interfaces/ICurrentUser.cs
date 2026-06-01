// QUAN-20260531-154643
namespace ONENET.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        string? UserId { get; }
        string? UserName { get; } // For audit trail
        bool IsInRole(string roleName);
        bool IsAuthenticated { get; }
    }
}