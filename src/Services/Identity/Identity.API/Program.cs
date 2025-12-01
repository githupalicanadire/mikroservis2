using Identity.API.Data;
using Identity.API.Models;
using Identity.API.Services;
using Identity.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Entity Framework with retry policy for SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)));

// ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings for development
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// IdentityServer4
builder.Services.AddIdentityServer(options =>
{
    options.IssuerUri = builder.Configuration["IdentityServer:IssuerUri"];
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
.AddInMemoryIdentityResources(Config.IdentityResources)
.AddInMemoryApiScopes(Config.ApiScopes)
.AddInMemoryApiResources(Config.ApiResources)
.AddInMemoryClients(Config.Clients)
.AddAspNetIdentity<ApplicationUser>()
.AddProfileService<ProfileService>()
.AddDeveloperSigningCredential(); // Only for development

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(
            "http://localhost:6005",
            "https://localhost:6005",
            "http://localhost:6004",
            "https://localhost:6004",
            "http://localhost:6006",
            "https://localhost:6006"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        // Wait for database to be ready with retry logic
        logger.LogInformation("Starting database initialization...");

        // First, try to create the database if it doesn't exist
        try
        {
            // This will create the database if it doesn't exist
            var connectionStringBuilder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(
                context.Database.GetConnectionString());
            var masterConnectionString = connectionStringBuilder.ToString().Replace(connectionStringBuilder.InitialCatalog, "master");

            using var masterConnection = new Microsoft.Data.SqlClient.SqlConnection(masterConnectionString);
            await masterConnection.OpenAsync();

            var createDbCommand = new Microsoft.Data.SqlClient.SqlCommand(
                $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{connectionStringBuilder.InitialCatalog}') " +
                $"CREATE DATABASE [{connectionStringBuilder.InitialCatalog}]", masterConnection);

            await createDbCommand.ExecuteNonQueryAsync();
            logger.LogInformation("Database existence verified/created");
        }
        catch (Exception dbCreateEx)
        {
            logger.LogWarning(dbCreateEx, "Could not create database manually, trying EnsureCreated");
        }

        // Now ensure the schema is created
        await context.Database.EnsureCreatedAsync();
        logger.LogInformation("Database schema ensured");

        // Seed demo data
        await app.SeedDemoDataAsync();
        logger.LogInformation("Demo data seeding completed");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database initialization");
        // Don't throw - let the application start anyway for debugging
    }
}

app.UseCors("AllowSpecificOrigins");
app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
