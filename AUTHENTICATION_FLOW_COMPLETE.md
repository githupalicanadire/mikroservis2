# 🔐 EShop Authentication Flow - Tam Kullanım Kılavuzu

## 🎯 3 Ana Durum: Login Olmadan, Login Sonrası, Register İşlemi

### 📋 İçindekiler

- [Login Olmadan Neler Oluyor](#login-olmadan-neler-oluyor)
- [Login Olduktan Sonra Neler Oluyor](#login-olduktan-sonra-neler-oluyor)
- [Register Olunca Neler Oluyor](#register-olunca-neler-oluyor)
- [Korumalı Sayfalar](#korumalı-sayfalar)
- [Token Yönetimi](#token-yönetimi)

---

## 🚫 Login Olmadan Neler Oluyor

### Erişilebilir Sayfalar (Herkes İçin)

```
✅ Home Page (/)           - Ürün kategorileri ve özellikler
✅ Product List (/ProductList)  - Tüm ürünleri görüntüleme
✅ Product Detail (/ProductDetail) - Ürün detayları
✅ Login (/Login)          - Giriş sayfası
✅ Register (/Register)    - Kayıt sayfası
✅ Contact (/Contact)      - İletişim sayfası
✅ Privacy (/Privacy)      - Gizlilik politikası
```

### Erişilemeyen Sayfalar (Korumalı)

```
❌ Cart (/Cart)           → Login sayfasına yönlendirir
❌ Checkout (/Checkout)   → Login sayfasına yönlendirir
❌ Order List (/OrderList) → Login sayfasına yönlendirir
❌ Order Detail (/OrderDetail) → Login sayfasına yönlendirir
```

### Navigation Bar Görünümü (Login Olmadan)

```html
<!-- Navbar'da görünen linkler -->
🏠 Home | 📦 Products | 🔐 Sign In | 👤 Register

<!-- Görünmeyen linkler -->
❌ Cart ❌ Orders ❌ Profile ❌ JWT Debug
```

### Korumalı Sayfaya Erişim Denemesi

```csharp
// Program.cs middleware - Otomatik yönlendirme
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();
    var isAuthenticated = context.User?.Identity?.IsAuthenticated == true;

    var protectedPaths = new[] { "/cart", "/checkout", "/orderlist", "/orderdetail" };

    if (!isAuthenticated && protectedPaths.Any(p => path?.StartsWith(p) == true))
    {
        var returnUrl = context.Request.Path + context.Request.QueryString;
        var message = "Please login to access your shopping cart";

        context.Response.Redirect($"/Login?returnUrl={Uri.EscapeDataString(returnUrl)}&message={Uri.EscapeDataString(message)}");
        return;
    }
    await next();
});
```

### API Çağrıları (Login Olmadan)

```csharp
// Catalog Service - Herkese açık
✅ GET /catalog-service/products           - Ürün listesi
✅ GET /catalog-service/products/{id}      - Ürün detayı
✅ GET /catalog-service/products/category/{cat} - Kategori ürünleri

// Basket/Order Services - Korumalı (Token gerekli)
❌ GET /basket-service/basket/{user}       - 401 Unauthorized
❌ POST /basket-service/basket             - 401 Unauthorized
❌ GET /ordering-service/orders/{id}       - 401 Unauthorized
```

---

## ✅ Login Olduktan Sonra Neler Oluyor

### 1. **Login İşlemi Adımları**

```csharp
// LoginModel.OnPostAsync()
public async Task<IActionResult> OnPostAsync()
{
    // 1. Model validation
    if (!ModelState.IsValid) return Page();

    // 2. Identity Server'a yönlendirme
    return Challenge(new AuthenticationProperties
    {
        RedirectUri = returnUrl ?? "/"
    }, "oidc");
}
```

### 2. **Identity Server'da Authentication**

```csharp
// Identity.API AccountController.Login
[HttpPost]
public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
{
    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user != null)
    {
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            // Token generation ve callback
            return Redirect(returnUrl);
        }
    }
    return View(model);
}
```

### 3. **Token Alma ve Saklama**

```csharp
// OpenID Connect configuration
.AddOpenIdConnect("oidc", options =>
{
    options.SaveTokens = true;  // Token'ları cookie'de sakla
    options.GetClaimsFromUserInfoEndpoint = true;

    // Scopes - hangi API'lere erişim
    options.Scope.Add("catalog.api");
    options.Scope.Add("basket.api");
    options.Scope.Add("ordering.api");
});
```

### 4. **Navigation Bar Değişimi (Login Sonrası)**

```html
<!-- Navbar'da g��rünen linkler -->
🏠 Home | 📦 Products | 🛒 Cart | 📋 Orders | 🔧 JWT Debug

<!-- User Dropdown -->
👤 Mehmet Özkaya ▼ ├─ 👤 Profile ├─ 📋 My Orders ├─ ───────────── └─ 🚪 Sign Out
```

### 5. **Tüm Sayfalar Erişilebilir**

```
✅ Home Page               - Ürünler ve kategoriler
✅ Product List           - Tüm ürünler + "Add to Cart" aktif
✅ Product Detail         - Ürün detayı + "Add to Cart" aktif
✅ Cart                   - Sepet yönetimi
✅ Checkout               - Ödeme işlemi
✅ Order List             - Sipariş geçmişi
✅ Order Detail           - Sipariş detayları
✅ JWT Debug              - Token bilgileri
```

### 6. **API Çağrıları (Login Sonrası)**

```csharp
// AuthenticatedHttpClientHandler - Her istekte token ekleme
protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request, CancellationToken cancellationToken)
{
    var accessToken = await httpContext.GetTokenAsync("access_token");
    if (!string.IsNullOrEmpty(accessToken))
    {
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
    }
    return await base.SendAsync(request, cancellationToken);
}

// Artık tüm API'ler erişilebilir
✅ GET /basket-service/basket/admin@toyshop.com  - Sepet getir
✅ POST /basket-service/basket                   - Sepet güncelle
✅ POST /basket-service/basket/checkout          - Ödeme yap
✅ GET /ordering-service/orders/customerId       - Siparişler
```

### 7. **User Claims ve Bilgiler**

```csharp
// User.Identity.IsAuthenticated = true
// Available Claims:
- sub: "user-id-guid"
- name: "Admin User"
- email: "admin@toyshop.com"
- given_name: "Admin"
- family_name: "User"
- scope: ["catalog.api", "basket.api", "ordering.api"]
```

---

## 👤 Register Olunca Neler Oluyor

### 1. **Register Form Submission**

```csharp
// RegisterModel.OnPostAsync()
public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid) return Page();

    // Identity API'ye HTTP POST
    var identityServerUrl = "http://localhost:6006";
    var registerUrl = $"{identityServerUrl}/api/auth/register";

    var registerRequest = new
    {
        RegisterData.FirstName,
        RegisterData.LastName,
        RegisterData.Email,
        RegisterData.Password,
        RegisterData.ConfirmPassword
    };

    var response = await _httpClient.PostAsync(registerUrl, content);

    if (response.IsSuccessStatusCode)
    {
        SuccessMessage = "Account created successfully!";
        return RedirectToPage("Login");
    }
}
```

### 2. **Identity API'de User Creation**

```csharp
// AuthController.Register
[HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
{
    var user = new ApplicationUser
    {
        UserName = model.Email,
        Email = model.Email,
        FirstName = model.FirstName,
        LastName = model.LastName,
        EmailConfirmed = true  // Demo için otomatik onay
    };

    var result = await _userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
        // Kullanıcı veritabanına kaydedildi
        return Ok(new { message = "User registered successfully" });
    }
}
```

### 3. **Register Sonrası Durum**

```
1. ✅ Kullanıcı SQL Server'da Identity tablosuna kaydedilir
2. ✅ "Account created successfully!" mesajı gösterilir
3. ✅ Otomatik olarak Login sayfasına yönlendirilir
4. ✅ Yeni kullanıcı artık giriş yapabilir
5. ✅ Demo hesap gibi tüm özelliklere erişim
```

### 4. **Database'de Yeni User**

```sql
-- AspNetUsers tablosunda yeni kayıt
INSERT INTO AspNetUsers (
    Id, UserName, Email, FirstName, LastName,
    EmailConfirmed, CreatedDate, IsActive
) VALUES (
    'new-guid', 'user@example.com', 'user@example.com',
    'John', 'Doe', 1, GETUTCDATE(), 1
);
```

---

## 🛡️ Korumalı Sayfalar ve Authorization

### Page-Level Authorization

```csharp
// Authorize attribute ile korumalı sayfalar
[Authorize]
public class CartModel : PageModel { }

[Authorize]
public class CheckoutModel : PageModel { }

[Authorize]
public class OrderListModel : PageModel { }

[Authorize]
public class OrderDetailModel : PageModel { }
```

### Custom Authorization Logic

```csharp
// Her korumalı sayfada kontrol
public async Task<IActionResult> OnGetAsync()
{
    if (!userService.IsAuthenticated())
    {
        return RedirectToPage("/Login");
    }

    var userName = userService.GetCurrentUserName() ?? userService.GetCurrentUserEmail();
    var userId = userService.GetCurrentUserId();

    // Business logic...
}
```

---

## 🎫 Token Yönetimi ve Lifecycle

### Access Token Storage

```csharp
// Cookies'de güvenli saklanıyor
options.SaveTokens = true;

// Token alma
var accessToken = await HttpContext.GetTokenAsync("access_token");
var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
var idToken = await HttpContext.GetTokenAsync("id_token");
```

### Token Validation

```csharp
// Her API çağrısında otomatik validasyon
// API Gateway -> Identity Server token check
// Backend Services -> Identity Server token check
```

### Token Expiration Handling

```csharp
// Token süresi dolduğunda
options.Events = new OpenIdConnectEvents
{
    OnAccessDenied = context =>
    {
        // Refresh token ile yenileme veya re-login
        return Task.CompletedTask;
    }
};
```

---

## 🔄 Logout İşlemi

### Logout Process

```csharp
// LogoutModel.OnPostAsync()
public async Task<IActionResult> OnPostAsync()
{
    // Hem cookie hem de Identity Server'dan çıkış
    return SignOut(new AuthenticationProperties
    {
        RedirectUri = Url.Page("/Index")
    }, "Cookies", "oidc");
}
```

### Logout Sonrası

```
1. ✅ Local cookies temizlenir
2. ✅ Identity Server session sonlandırılır
3. ✅ Home page'e yönlendirilir
4. ✅ Navigation bar login öncesi duruma döner
5. ✅ Korumalı sayfalar tekrar erişilemez olur
```

---

## 🎭 Gerçek Kullanım Senaryoları

### Senaryo 1: Misafir Kullanıcı (Login Olmadan)

```
1. Site'ye gelir, ürünleri görür
2. Cart'a tıklar → Login sayfasına yönlendirilir
3. "Please login to access your shopping cart" mesajı
4. Login yapar veya Register olur
5. Başarılı girişten sonra Cart sayfası açılır
```

### Senaryo 2: Kayıtlı Kullanıcı (Login Sonrası)

```
1. Login yapar (admin@toyshop.com / Admin123!)
2. Tüm sayfalar erişilebilir olur
3. Ürün sepete ekler, checkout yapar
4. Sipariş verir, order history görür
5. İstediği zaman logout yapar
```

### Senaryo 3: Yeni Kullanıcı (Register)

```
1. Register sayfasında form doldurur
2. Identity API'de yeni kullanıcı oluşturulur
3. "Account created successfully!" mesajı
4. Login sayfasına yönlendirilir
5. Yeni bilgileri ile giriş yapar
```

## 🎯 Özet: Tam Authentication Flow

**Login Olmadan:** Sadece ürün görüntüleme, giriş/kayıt  
**Login Sonrası:** Tüm özellikler aktif, sepet, sipariş, profil  
**Register Sonrası:** Yeni hesap oluşturulur, giriş yapabilir

Bu sistem **enterprise-level security** ile tam korumalı bir e-ticaret deneyimi sunuyor! 🛡️🛒
