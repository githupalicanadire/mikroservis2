namespace Shopping.Web.Services;

public interface IUserService
{
    string? GetCurrentUserName();
    string? GetCurrentUserId();
    string? GetCurrentUserEmail();
    bool IsAuthenticated();
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
            // Try to get user ID from claims
            return context.User.FindFirst("sub")?.Value ?? 
                   context.User.FindFirst("id")?.Value;
        }
        return null;
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
}
