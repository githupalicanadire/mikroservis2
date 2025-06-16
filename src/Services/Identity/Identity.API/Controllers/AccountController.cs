using Identity.API.Models;
using Identity.API.ViewModels;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly IEventService _events;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        IEventService events,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
        _clientStore = clientStore;
        _events = events;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

        var vm = new AccountLoginViewModel();

        if (context?.IdP != null && await SchemeSupportsSignOut(context.IdP))
        {
            vm.EnableLocalLogin = false;
            vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
        }

        ViewData["ReturnUrl"] = returnUrl;
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AccountLoginViewModel model, string? returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

                    if (context != null)
                    {
                        return Redirect(returnUrl!);
                    }

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return Redirect("~/");
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Email, "invalid credentials", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            else
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(model.Email, "invalid username", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }

        ViewData["ReturnUrl"] = returnUrl;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string? logoutId)
    {
        var logout = await _interaction.GetLogoutContextAsync(logoutId);

        if (User?.Identity?.IsAuthenticated == true)
        {
            await _signInManager.SignOutAsync();
            await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
        }

        var postLogoutRedirectUri = logout?.PostLogoutRedirectUri;

        if (!string.IsNullOrEmpty(postLogoutRedirectUri))
        {
            return Redirect(postLogoutRedirectUri);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl ?? "~/");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    private async Task<bool> SchemeSupportsSignOut(string scheme)
    {
        var provider = HttpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
        var handler = await provider.GetHandlerAsync(HttpContext, scheme);
        return handler is IAuthenticationSignOutHandler;
    }
}

public class ExternalProvider
{
    public string? DisplayName { get; set; }
    public string AuthenticationScheme { get; set; } = default!;
}

public class AccountLoginViewModel : LoginViewModel
{
    public bool EnableLocalLogin { get; set; } = true;
    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
    public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));
    public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
    public string? ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
}
