// QUAN-20260531-154643
namespace ONENET.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        string? UserId { get; }
        string? UserName { get; }
        string?[] Roles { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}