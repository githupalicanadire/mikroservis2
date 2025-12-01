namespace Shopping.Web.Services;

public interface IUserService
{
    string? GetCurrentUserName();
    string? GetCurrentUserId();
    string? GetCurrentUserEmail();
    bool IsAuthenticated();
    bool ValidateUserOwnership(string resourceUserId);
    string GetSecureUserIdentifier();
}

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentUserName()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context?.User?.Identity?.IsAuthenticated == true)
        {
            // Try to get name from claims
            return context.User.FindFirst("name")?.Value ??
                   context.User.FindFirst("email")?.Value ??
                   context.User.Identity.Name;
        }
        return null;
    }

    public string? GetCurrentUserId()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context?.User?.Identity?.IsAuthenticated == true)
        {
            // Try to get user ID from claims (sub is the standard for user ID in JWT)
            var userId = context.User.FindFirst("sub")?.Value ??
                        context.User.FindFirst("id")?.Value ??
                        context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                return userId;
            }

            // Fallback: Generate consistent GUID from email for demo purposes
            var email = GetCurrentUserEmail();
            if (!string.IsNullOrEmpty(email))
            {
                return GenerateConsistentGuidFromEmail(email);
            }
        }
        return null;
    }

    private string GenerateConsistentGuidFromEmail(string email)
    {
        // Generate a consistent GUID from email for demo purposes
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(email));
        var guid = new Guid(hash.Take(16).ToArray());
        return guid.ToString();
    }

    public string? GetCurrentUserEmail()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context?.User?.Identity?.IsAuthenticated == true)
        {
            return context.User.FindFirst("email")?.Value;
        }
        return null;
    }

    public bool IsAuthenticated()
    {
        var context = _httpContextAccessor.HttpContext;
        return context?.User?.Identity?.IsAuthenticated == true;
    }

    public bool ValidateUserOwnership(string resourceUserId)
    {
        var currentUserId = GetCurrentUserId();
        return !string.IsNullOrEmpty(currentUserId) &&
               currentUserId.Equals(resourceUserId, StringComparison.OrdinalIgnoreCase);
    }

    public string GetSecureUserIdentifier()
    {
        if (!IsAuthenticated())
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("Cannot determine user identity");
        }

        return userId;
    }
}
