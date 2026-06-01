// QUAN-20260530-2301
using Microsoft.AspNetCore.Authorization;

namespace ONENET.WebAPI.Filters;

// Custom Authorize attribute for multiple roles.
// Assumes standard ASP.NET Core Identity/JWT setup where roles are in claims.
public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}