// QUAN-20260530-2301
namespace ONENET.Application.Common.Interfaces;

public interface ICurrentUser
{
    Guid? UserId { get; }
    string? UserName { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(string roleName);
}