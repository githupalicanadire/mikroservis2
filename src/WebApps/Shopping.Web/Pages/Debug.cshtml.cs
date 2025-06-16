using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages;

public class DebugModel : PageModel
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? TokenType { get; set; }
    public string? ExpiresAt { get; set; }

    public async Task OnGetAsync()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            AccessToken = await HttpContext.GetTokenAsync("access_token");
            RefreshToken = await HttpContext.GetTokenAsync("refresh_token");
            TokenType = await HttpContext.GetTokenAsync("token_type");
            ExpiresAt = await HttpContext.GetTokenAsync("expires_at");
        }
    }
}
