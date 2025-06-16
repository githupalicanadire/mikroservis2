using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace Shopping.Web.Pages;

public class RegisterModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(HttpClient httpClient, IConfiguration configuration, ILogger<RegisterModel> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    [BindProperty]
    public RegisterInputModel RegisterData { get; set; } = default!;

    [TempData]
    public string? SuccessMessage { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var identityServerUrl = _configuration["IdentityServer:BaseUrl"] ?? "http://localhost:6006";
            var registerUrl = $"{identityServerUrl}/api/auth/register";

            var registerRequest = new
            {
                RegisterData.FirstName,
                RegisterData.LastName,
                RegisterData.Email,
                RegisterData.Password,
                RegisterData.ConfirmPassword
            };

            var json = JsonSerializer.Serialize(registerRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(registerUrl, content);

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Account created successfully! You can now sign in.";
                return RedirectToPage("Login");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ErrorMessage = "Registration failed. Please try again.";
                return Page();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during registration for user {Email}", RegisterData.Email);
            ErrorMessage = "An error occurred during registration. Please try again.";
            return Page();
        }
    }
}

public class RegisterInputModel
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = default!;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = default!;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = default!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = default!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = default!;
}
