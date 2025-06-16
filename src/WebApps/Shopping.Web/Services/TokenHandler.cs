using Microsoft.AspNetCore.Authentication;

namespace Shopping.Web.Services;

public class AuthenticatedHttpClientHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthenticatedHttpClientHandler> _logger;

    public AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor, ILogger<AuthenticatedHttpClientHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        
        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            try
            {
                // Get the access token from the current user context
                var accessToken = await httpContext.GetTokenAsync("access_token");
                
                if (!string.IsNullOrEmpty(accessToken))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    _logger.LogDebug("Added Bearer token to request for {RequestUri}", request.RequestUri);
                }
                else
                {
                    _logger.LogWarning("No access token found for authenticated user");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving access token");
            }
        }
        else
        {
            _logger.LogDebug("User not authenticated, skipping token attachment");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
