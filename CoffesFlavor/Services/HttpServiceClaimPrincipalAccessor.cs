using System.Security.Claims;

namespace CoffesFlavor.Services
{
    public class HttpServiceClaimPrincipalAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpServiceClaimPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetClaim()
        {
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

    }
}
