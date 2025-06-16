using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to use only HTTP
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

// Add JWT Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["IdentityServer:BaseUrl"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["IdentityServer:BaseUrl"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        options.RequireHttpsMetadata = false; // Only for development
    });

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("catalog.api", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "catalog.api");
    });
});

builder.Services.AddCarter();

// Add Swagger with OAuth2
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName?.Replace("+", "_"));

    // Add OAuth2 Security Definition
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{builder.Configuration["IdentityServer:BaseUrl"] ?? "http://localhost:6006"}/connect/authorize"),
                TokenUrl = new Uri($"{builder.Configuration["IdentityServer:BaseUrl"] ?? "http://localhost:6006"}/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "catalog.api", "Catalog API Access" },
                    { "openid", "OpenID" },
                    { "profile", "Profile" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { "catalog.api" }
        }
    });
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
    c.RoutePrefix = "swagger";

    // OAuth2 Configuration
    c.OAuthClientId("swagger.ui");
    c.OAuthUsePkce();
    c.OAuthScopeSeparator(" ");
    c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
    {
        { "nonce", Guid.NewGuid().ToString() }
    });
});

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
