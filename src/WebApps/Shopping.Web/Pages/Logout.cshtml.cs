using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages;

public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        return await OnPostAsync();
    }

    public Task<IActionResult> OnPostAsync()
    {
        // Sign out from both cookies and OIDC
        return Task.FromResult<IActionResult>(SignOut(new AuthenticationProperties
        {
            RedirectUri = Url.Page("/Index")
        }, "Cookies", "oidc"));
    }
}
