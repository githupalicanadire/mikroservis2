# 🔗 EShop Microservices İlişki Haritası ve Kimlik Doğrulama Akışı

## 🏗️ Mimarinin Genel Yapısı

```
┌─────────────────┐    ┌──────────────────┐    ┌─────────────────┐
│   Shopping.Web  │───▶│   Identity API   │───▶│   SQL Server    │
│   (Frontend)    │    │ (IdentityServer4)│    │  (Identity DB)  │
│   Port: 6005    │    │   Port: 6006     │    │   Port: 1434    │
└─────────────────┘    └──────────────────┘    └─────────────────┘
         │                       ▲
         │                       │ Token Validation
         ▼                       │
┌─────────────────┐    ┌──────────────────┐
│   API Gateway   │───▶│  All Backend     │
│  (YARP Proxy)   │    │    Services      │
│   Port: 6004    │    │                  │
└─────────────────┘    └──────────────────┘
         │                       │
         ▼                       ▼
┌─────────────────────────────────────────┐
│            Backend Services             │
│  ┌─────────┐ ┌─────────┐ ┌─────────┐   │
│  │Catalog  │ │ Basket  │ │Ordering │   │
│  │  6000   │ │  6001   │ │  6003   │   │
│  └─────────┘ └─────────┘ └─────────┘   │
│               ┌─────────┐               │
│               │Discount │               │
│               │  6002   │               │
│               └─────────┘               │
└─────────────────────────────────────────┘
```

## 🔐 Identity API ile Diğer Servislerin İlişkisi

### 1. **Shopping.Web ↔ Identity API**

```csharp
// Shopping.Web appsettings.json
"IdentityServer": {
    "BaseUrl": "http://localhost:6006"
}

// Program.cs - OpenID Connect yapılandırması
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "http://localhost:6006";
    options.ClientId = "shopping.web";
    options.ClientSecret = "secret";
    options.Scope.Add("catalog.api");
    options.Scope.Add("basket.api");
    options.Scope.Add("ordering.api");
})
```

### 2. **Backend Servislerin Identity API ile Token Doğrulama**

```csharp
// Her backend servis Program.cs'inde
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://identity.api:8080";
        options.RequireHttpsMetadata = false;
        options.Audience = "catalog.api"; // Her servis kendi audience'ı
    });
```

### 3. **API Gateway ↔ Identity API**

```yaml
# YARP Configuration
Routes:
  - RouteId: "catalog-route"
    ClusterId: "catalog-cluster"
    AuthorizationPolicy: "RequireAuthenticatedUser"

Authentication:
  DefaultScheme: "Bearer"
  Bearer:
    Authority: "http://identity.api:8080"
```

## 🌐 UI'ın Tüm Servislerle İlişkisi

### 1. **Shopping.Web → API Gateway → Backend Services**

```csharp
// Shopping.Web Services Registration
builder.Services.AddRefitClient<ICatalogService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:6004"))
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IBasketService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:6004"))
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IOrderingService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:6004"))
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
```

### 2. **AuthenticatedHttpClientHandler - Token Ekleme**

```csharp
protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request, CancellationToken cancellationToken)
{
    if (httpContext?.User?.Identity?.IsAuthenticated == true)
    {
        var accessToken = await httpContext.GetTokenAsync("access_token");
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
    }
    return await base.SendAsync(request, cancellationToken);
}
```

## 🔄 Tam Kimlik Doğrulama Akışı

### Adım 1: Kullanıcı Giriş İşlemi

```
1. User → Shopping.Web/Login
2. Shopping.Web → Identity API/Account/Login (redirect)
3. User enters credentials (admin@toyshop.com / Admin123!)
4. Identity API validates → Issues token
5. Identity API → Shopping.Web (callback with token)
6. Shopping.Web stores token in cookies
```

### Adım 2: API Çağrıları

```
1. Shopping.Web → API Gateway (with Bearer token)
2. API Gateway → Identity API (token validation)
3. API Gateway → Backend Service (forwarded request)
4. Backend Service → Identity API (token validation)
5. Backend Service → Database
6. Response chain back to UI
```

## 📊 Servis İletişim Matrisi

| Kaynak Servis | Hedef Servis  | İletişim Türü  | Kimlik Doğrulama      |
| ------------- | ------------- | -------------- | --------------------- |
| Shopping.Web  | Identity API  | OpenID Connect | Kullanıcı Credentials |
| Shopping.Web  | API Gateway   | HTTP + Bearer  | Access Token          |
| API Gateway   | Catalog API   | HTTP + Bearer  | Token Forward         |
| API Gateway   | Basket API    | HTTP + Bearer  | Token Forward         |
| API Gateway   | Ordering API  | HTTP + Bearer  | Token Forward         |
| API Gateway   | Identity API  | HTTP           | Token Validation      |
| Catalog API   | Identity API  | HTTP           | Token Validation      |
| Basket API    | Identity API  | HTTP           | Token Validation      |
| Basket API    | Discount gRPC | gRPC           | Internal              |
| Ordering API  | Identity API  | HTTP           | Token Validation      |

## 🔑 Scope ve Claims Yönetimi

### Identity Server Scopes

```csharp
public static IEnumerable<ApiScope> ApiScopes =>
    new List<ApiScope>
    {
        new ApiScope("catalog.api", "Catalog API"),
        new ApiScope("basket.api", "Basket API"),
        new ApiScope("ordering.api", "Ordering API"),
        new ApiScope("discount.grpc", "Discount gRPC"),
        new ApiScope("shopping.web", "Shopping Web")
    };
```

### Client Permissions

```csharp
AllowedScopes = {
    IdentityServerConstants.StandardScopes.OpenId,
    IdentityServerConstants.StandardScopes.Profile,
    IdentityServerConstants.StandardScopes.Email,
    "catalog.api",   // Product browsing
    "basket.api",    // Cart operations
    "ordering.api",  // Order management
    "shopping.web"   // Web app access
}
```

## 🛡️ Güvenlik Katmanları

### 1. **Authentication (Kimlik Doğrulama)**

- Identity Server IdentityServer4 ile
- JWT Bearer tokens
- OpenID Connect protocol

### 2. **Authorization (Yetkilendirme)**

- Scope-based permissions
- Role-based claims (future enhancement)
- Resource-based authorization

### 3. **API Protection**

- Bearer token validation
- CORS policies
- HTTPS enforcement (production)

## 🔧 Yapılandırma Dosyaları İlişkisi

### Shopping.Web appsettings.json

```json
{
  "ApiSettings": {
    "GatewayAddress": "http://localhost:6004"
  },
  "IdentityServer": {
    "BaseUrl": "http://localhost:6006"
  }
}
```

### Docker Compose Service Dependencies

```yaml
shopping.web:
  depends_on:
    - yarpapigateway
    - identity.api
  environment:
    - IdentityServer__BaseUrl=http://identity.api:8080
```

## 🎯 Kritik Noktalar

1. **Token Lifecycle Management**: Access/Refresh token handling
2. **Cross-Service Communication**: Service-to-service authentication
3. **Error Handling**: Authentication failures, token expiration
4. **Performance**: Token caching, connection pooling
5. **Security**: HTTPS, CORS, token storage

Bu mimari tam bir **Zero Trust** yaklaşımı ile her servisin kimlik doğrulaması yapılması ve token-based güvenlik sağlanmasını gerçekleştiriyor! 🛡️
