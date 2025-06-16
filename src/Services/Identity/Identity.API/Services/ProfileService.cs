using Identity.API.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Identity.API.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        if (user == null)
            return;

        var claims = new List<Claim>
        {
            new Claim("sub", user.Id),
            new Claim("email", user.Email ?? ""),
            new Claim("name", user.FullName),
            new Claim("given_name", user.FirstName),
            new Claim("family_name", user.LastName),
            new Claim("email_verified", user.EmailConfirmed.ToString().ToLower())
        };

        // Add user roles
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim("role", role));
        }

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        context.IsActive = user?.IsActive == true;
    }
}
