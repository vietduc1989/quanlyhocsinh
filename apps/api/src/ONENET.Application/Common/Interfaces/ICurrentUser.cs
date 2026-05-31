// QUAN-20260530-2301
using System;

namespace ONENET.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        string? UserId { get; }
        string? UserName { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string roleName);
    }
}