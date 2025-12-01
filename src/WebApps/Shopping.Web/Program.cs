using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add HttpClient
builder.Services.AddHttpClient();

// Add HttpContextAccessor and UserService
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<AuthenticatedHttpClientHandler>();

// Add Authentication with Identity Server
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.AccessDeniedPath = "/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;
})
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = builder.Configuration["IdentityServer:BaseUrl"];
    options.ClientId = "shopping.web";
    options.ClientSecret = "secret";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.RequireHttpsMetadata = false;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.Scope.Add("shopping.web");
    options.Scope.Add("catalog.api");
    options.Scope.Add("basket.api");
    options.Scope.Add("ordering.api");
    options.UseTokenLifetime = true;
    options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProvider = context =>
        {
            // Eğer login sayfasından geliyorsa, normal akışa devam et
            if (context.Request.Path.StartsWithSegments("/Login"))
            {
                return Task.CompletedTask;
            }

            // Diğer durumlarda önce login sayfasına yönlendir
            context.Response.Redirect("/Login?returnUrl=" + Uri.EscapeDataString(context.Request.Path + context.Request.QueryString));
            context.HandleResponse();
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddRefitClient<ICatalogService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    })
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IBasketService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    })
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IOrderingService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["ApiSettings:GatewayAddress"]!);
    })
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Add custom middleware to handle unauthorized access to protected pages
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();
    var isAuthenticated = context.User?.Identity?.IsAuthenticated == true;

    // Protected paths that require authentication
    var protectedPaths = new[] { "/cart", "/checkout", "/orderlist", "/orderdetail" };

    if (!isAuthenticated && protectedPaths.Any(p => path?.StartsWith(p) == true))
    {
        // Store the original URL to redirect after login
        var returnUrl = context.Request.Path + context.Request.QueryString;

        // Add a message to inform the user why they are being redirected
        string message;
        if (path?.StartsWith("/cart") == true)
        {
            message = "Please login to access your shopping cart";
        }
        else if (path?.StartsWith("/orderlist") == true || path?.StartsWith("/orderdetail") == true)
        {
            message = "Please login to view your orders";
        }
        else if (path?.StartsWith("/checkout") == true)
        {
            message = "Please login to complete your checkout";
        }
        else
        {
            message = "Please login to continue";
        }

        context.Response.Redirect($"/Login?returnUrl={Uri.EscapeDataString(returnUrl)}&message={Uri.EscapeDataString(message)}");
        return;
    }

    await next();
});

app.MapRazorPages();

app.Run();
