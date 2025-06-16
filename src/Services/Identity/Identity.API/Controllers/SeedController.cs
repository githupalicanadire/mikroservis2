using Identity.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeedController : ControllerBase
{
    private readonly ILogger<SeedController> _logger;
    private readonly IWebHostEnvironment _environment;

    public SeedController(ILogger<SeedController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetDemoUsers()
    {
        try
        {
            var app = HttpContext.RequestServices.GetRequiredService<IHost>() as WebApplication;
            var users = await app!.GetDemoUsersAsync();
            
            return Ok(new
            {
                success = true,
                count = users.Count,
                users = users.Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.FullName,
                    u.CreatedDate,
                    // Mask password for security
                    DefaultPassword = u.Email == "admin@toyshop.com" ? "Admin123!" : "User123!"
                })
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving demo users");
            return StatusCode(500, new { success = false, message = "Error retrieving demo users" });
        }
    }

    [HttpPost("reseed")]
    public async Task<IActionResult> ReseedData()
    {
        try
        {
            if (!_environment.IsDevelopment())
            {
                return BadRequest(new { success = false, message = "Reseeding is only allowed in development environment" });
            }

            var app = HttpContext.RequestServices.GetRequiredService<IHost>() as WebApplication;
            await app!.SeedDemoDataAsync();
            
            _logger.LogInformation("Demo data reseeded successfully");
            
            return Ok(new
            {
                success = true,
                message = "Demo data reseeded successfully",
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reseeding demo data");
            return StatusCode(500, new { success = false, message = "Error reseeding demo data" });
        }
    }

    [HttpGet("status")]
    public IActionResult GetSeedStatus()
    {
        return Ok(new
        {
            success = true,
            environment = _environment.EnvironmentName,
            timestamp = DateTime.UtcNow,
            identityServer = new
            {
                issuer = "http://localhost:6006",
                endpoints = new
                {
                    authorization = "http://localhost:6006/connect/authorize",
                    token = "http://localhost:6006/connect/token",
                    userinfo = "http://localhost:6006/connect/userinfo",
                    wellKnown = "http://localhost:6006/.well-known/openid-configuration"
                }
            },
            demoUsers = new[]
            {
                new { email = "admin@toyshop.com", password = "Admin123!", role = "Admin" },
                new { email = "john.doe@example.com", password = "User123!", role = "User" },
                new { email = "jane.smith@example.com", password = "User123!", role = "User" },
                new { email = "mike.wilson@example.com", password = "User123!", role = "User" },
                new { email = "sarah.johnson@example.com", password = "User123!", role = "User" }
            }
        });
    }
}
