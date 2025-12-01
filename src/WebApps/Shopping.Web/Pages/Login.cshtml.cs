using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Web.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginInputModel LoginData { get; set; } = default!;

    public IActionResult OnGet(string? returnUrl = null)
    {
        // If user is already authenticated, redirect to return URL or home
        if (User.Identity?.IsAuthenticated == true)
        {
            return LocalRedirect(returnUrl ?? "/");
        }

        // Store returnUrl in TempData for use in OnPost
        if (!string.IsNullOrEmpty(returnUrl))
        {
            TempData["ReturnUrl"] = returnUrl;
        }

        return Page();
    }

    public Task<IActionResult> OnPostAsync()
    {
        // If user is already authenticated, redirect to home
        if (User.Identity?.IsAuthenticated == true)
        {
            return Task.FromResult<IActionResult>(RedirectToPage("/Index"));
        }

        if (!ModelState.IsValid)
        {
            return Task.FromResult<IActionResult>(Page());
        }

        // Get the return URL from TempData or default to Index
        var returnUrl = TempData["ReturnUrl"]?.ToString() ?? Url.Page("/Index");

        // Redirect to Identity Server for authentication
        return Task.FromResult<IActionResult>(Challenge(new AuthenticationProperties
        {
            RedirectUri = returnUrl
        }, "oidc"));
    }
}

public class LoginInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    public bool RememberMe { get; set; }
}
