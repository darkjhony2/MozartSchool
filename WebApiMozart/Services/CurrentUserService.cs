using ColegioMozart.Application.Common.Interfaces;

namespace WebApiMozart.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string? UserId => _httpContextAccessor?.HttpContext?.Request?.Headers["UserId"];
    }
}
