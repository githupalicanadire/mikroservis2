using Identity.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity.API.Models;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<HealthController> _logger;

    public HealthController(
        ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager,
        ILogger<HealthController> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            // Check database connectivity
            var canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                return StatusCode(500, new { status = "Unhealthy", message = "Cannot connect to database" });
            }

            // Check if database tables exist
            var tablesExist = await _context.Users.AnyAsync();
            
            // Count users
            var userCount = await _userManager.Users.CountAsync();

            return Ok(new
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow,
                database = new
                {
                    canConnect = true,
                    tablesExist = true,
                    userCount = userCount
                },
                version = "1.0.0"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed");
            return StatusCode(500, new 
            { 
                status = "Unhealthy", 
                message = ex.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }

    [HttpGet("database")]
    public async Task<IActionResult> DatabaseStatus()
    {
        try
        {
            var connectionString = _context.Database.GetConnectionString();
            var canConnect = await _context.Database.CanConnectAsync();
            
            if (!canConnect)
            {
                return StatusCode(500, new 
                { 
                    status = "Cannot connect",
                    connectionString = connectionString?.Substring(0, Math.Min(50, connectionString.Length)) + "...",
                    timestamp = DateTime.UtcNow
                });
            }

            // Try to get pending migrations
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync();

            return Ok(new
            {
                status = "Connected",
                canConnect = true,
                pendingMigrations = pendingMigrations.ToList(),
                appliedMigrations = appliedMigrations.ToList(),
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database status check failed");
            return StatusCode(500, new 
            { 
                status = "Error", 
                message = ex.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }
}
