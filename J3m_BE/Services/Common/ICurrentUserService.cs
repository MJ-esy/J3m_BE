using System.Security.Claims;

namespace J3m_BE.Services.Common
{
    // Service to access information about the current authenticated user
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }

    public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
    {
        public string? UserId => accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
