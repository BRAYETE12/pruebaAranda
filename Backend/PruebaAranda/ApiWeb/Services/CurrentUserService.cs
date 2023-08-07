using Application.Common.Interfaces;
using System.Security.Claims;

namespace ApiWeb.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            var id = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(q => q.Type == ClaimTypes.Sid)?
                .Value;

            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";

            User = new CurrentUser(id, userName);
        }

        CurrentUser User { get; }

        CurrentUser ICurrentUserService.User => User;

        public bool IsInRole(string roleName) =>
            _httpContextAccessor.HttpContext!.User.IsInRole(roleName);
    }
}
