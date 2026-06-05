// QUAN-20260604-153038
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ONENET.WebAPI.Models;
using System.Linq;

namespace ONENET.WebAPI.Filters
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string? Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Skip authorization if action has [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // Authorization
            var user = context.HttpContext.User;
            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                // Not authenticated
                context.Result = new UnauthorizedObjectResult(ApiResponse<object>.Failure("Yêu cầu xác thực.", "Unauthorized"));
                return;
            }

            if (!string.IsNullOrWhiteSpace(Roles))
            {
                var requiredRoles = Roles.Split(',');
                var userRoles = user.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();

                if (!requiredRoles.Any(r => userRoles.Contains(r)))
                {
                    // Not authorized based on roles
                    context.Result = new ForbidResult(); // Returns 403 Forbidden
                    return;
                }
            }
        }
    }
}