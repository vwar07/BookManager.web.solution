using System.Security.Claims;

namespace Project.Tracker.Web.Services.IdentityServices
{
    public class UserResolverService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public UserResolverService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        internal string GetUserFirstName()
        {
			if (_httpContextAccessor != null &&
		   _httpContextAccessor.HttpContext != null &&
		   _httpContextAccessor.HttpContext.User != null)
			{
				var firstName = _httpContextAccessor.HttpContext.User.Claims
								.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;

				return firstName ?? string.Empty;  // Return the first name or empty string if not found
			}
			else
			{
				return string.Empty;
			}
		}

		internal string GetUserName()
		{
			if (_httpContextAccessor != null
				&& _httpContextAccessor.HttpContext != null
				&& _httpContextAccessor.HttpContext.User != null
				&& _httpContextAccessor.HttpContext.User.Identity != null)
			{
				return _httpContextAccessor.HttpContext.User?.Identity?.Name;
			}
			else
			{
				return string.Empty;
			}
		}

		internal int? GetUserId() // int will  be null if the user is not authenticated
		{
			var userIdClaimValue = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (!string.IsNullOrEmpty(userIdClaimValue))
			{
				return int.Parse(userIdClaimValue);
			}
			else
			{
				return null;
			}
		}
	}
}
