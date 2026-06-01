// QUAN-20260530-2301
using System.Security.Claims;
using ONENET.Application.Common.Interfaces;
using ONENET.Infrastructure.Services; // Ensure CurrentUserService is accessible

namespace ONENET.WebAPI.Middleware;

// This middleware is necessary to make ICurrentUser work correctly within the request pipeline.
// In a real application, the ICurrentUser implementation would read from HttpContext.User.
// We are injecting a concrete CurrentUserService, and it needs access to HttpContext.
public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ICurrentUser currentUser)
    {
        // For demonstration, manually populate CurrentUserService.
        // In a real application, ICurrentUser would be populated automatically
        // after authentication middleware has run and populated HttpContext.User.
        // Assuming CurrentUserService implicitly uses IHttpContextAccessor.

        // If your ICurrentUser is directly dependent on HttpContext.User (which it should be),
        // you might not need to do anything explicit here other than ensuring it's scoped.
        // This middleware is more of a placeholder to illustrate the concept.

        // Make sure HttpContextAccessor is registered in Program.cs: services.AddHttpContextAccessor();
        await _next(context);
    }
}