using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ONENET.Application.Common.Interfaces;

namespace ONENET.Infrastructure.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public bool IsInRole(string roleName)
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole(roleName) ?? false;
        }
    }
}