using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Data;

public static class SeedDataExtensions
{
    public static async Task SeedDemoDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        
        try
        {
            await SeedUsersAsync(services, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database with demo data");
        }
    }

    private static async Task SeedUsersAsync(IServiceProvider services, ILogger logger)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        
        logger.LogInformation("Starting to seed demo users...");

        var demoUsers = new[]
        {
            new
            {
                Email = "admin@toyshop.com",
                FirstName = "Admin",
                LastName = "User",
                Password = "Admin123!"
            },
            new
            {
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe", 
                Password = "User123!"
            },
            new
            {
                Email = "jane.smith@example.com",
                FirstName = "Jane",
                LastName = "Smith",
                Password = "User123!"
            },
            new
            {
                Email = "mike.wilson@example.com", 
                FirstName = "Mike",
                LastName = "Wilson",
                Password = "User123!"
            },
            new
            {
                Email = "sarah.johnson@example.com",
                FirstName = "Sarah", 
                LastName = "Johnson",
                Password = "User123!"
            }
        };

        foreach (var userData in demoUsers)
        {
            var existingUser = await userManager.FindByEmailAsync(userData.Email);
            if (existingUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = userData.Email,
                    Email = userData.Email,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    EmailConfirmed = true,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user, userData.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("Created demo user: {Email} (ID: {UserId})", userData.Email, user.Id);
                }
                else
                {
                    logger.LogError("Failed to create demo user {Email}: {Errors}", 
                        userData.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                logger.LogInformation("Demo user already exists: {Email} (ID: {UserId})", userData.Email, existingUser.Id);
            }
        }

        logger.LogInformation("Demo user seeding completed");
    }

    public static async Task<List<UserInfo>> GetDemoUsersAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var users = new List<UserInfo>();
        var demoEmails = new[]
        {
            "admin@toyshop.com",
            "john.doe@example.com", 
            "jane.smith@example.com",
            "mike.wilson@example.com",
            "sarah.johnson@example.com"
        };

        foreach (var email in demoEmails)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                users.Add(new UserInfo
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    CreatedDate = user.CreatedDate
                });
            }
        }

        return users;
    }
}

public class UserInfo
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
}
