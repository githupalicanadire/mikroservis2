using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        // Add JWT Authentication
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = configuration["IdentityServer:BaseUrl"];
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = configuration["IdentityServer:BaseUrl"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.RequireHttpsMetadata = false; // Only for development
            });

        // Add Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ordering.api", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "ordering.api");
            });
        });

        // Add Swagger with OAuth2
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Ordering API", Version = "v1" });

            // Add OAuth2 Security Definition
            c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
                Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
                {
                    AuthorizationCode = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{configuration["IdentityServer:BaseUrl"] ?? "http://localhost:6006"}/connect/authorize"),
                        TokenUrl = new Uri($"{configuration["IdentityServer:BaseUrl"] ?? "http://localhost:6006"}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
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
                    new[] { "ordering.api" }
                }
            });
        });

        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure Swagger UI
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering API V1");
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapCarter();

        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        return app;
    }
}
