using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Add Swagger for API Gateway
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API Gateway", Version = "v1" });

    // Add OAuth2 Security Definition
    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
        Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
        {
            AuthorizationCode = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{builder.Configuration["IdentityServer:BaseUrl"] ?? "http://localhost:6006"}/connect/authorize"),
                TokenUrl = new Uri($"{builder.Configuration["IdentityServer:BaseUrl"] ?? "http://localhost:6006"}/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "gateway.api", "Gateway API Access" },
                    { "catalog.api", "Catalog API Access" },
                    { "basket.api", "Basket API Access" },
                    { "ordering.api", "Ordering API Access" },
                    { "openid", "OpenID" },
                    { "profile", "Profile" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { "gateway.api" }
        }
    });
});

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1");
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
}

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
