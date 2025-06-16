# 🔐 EShop Identity Server Kurulum ve Çözüm Kılavuzu

## ✅ Yapılan Düzeltmeler

### 1. Identity Server Yapılandırması

- ✅ IdentityServer4 client yapılandırması güncellendi
- ✅ Shopping.Web için doğru redirect URI'ları eklendi
- ✅ Development ortamı için port yapılandırması (6006) düzeltildi
- ✅ CORS politikaları güncellendi

### 2. Shopping.Web Authentication

- ✅ OpenID Connect yapılandırması düzeltildi
- ✅ Identity Server base URL'i localhost:6006 olarak ayarlandı
- ✅ Development appsettings.json olu��turuldu
- ✅ AuthenticatedHttpClientHandler token yönetimi

### 3. Identity API Geliştirmeleri

- ✅ MVC support eklendi (Views, Controllers)
- ✅ Modern login/register sayfaları oluşturuldu
- ✅ Account Controller ve ViewModels tamamlandı
- ✅ Demo kullanıcı hesabı (admin@toyshop.com / Admin123!)

### 4. Database Yapılandırması

- ✅ SQL Server bağlantı dizgileri yapılandırıldı
- ✅ Entity Framework migrations hazır
- ✅ Otomatik veritabanı oluşturma ve seed data

## 🚀 Çalıştırma Seçenekleri

### Seçenek 1: Docker ile Tam Ortam (Önerilen)

```bash
# Proje kök dizininde
cd src
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

# Servisler çalıştıktan sonra:
# - Shopping Web: http://localhost:6005
# - Identity Server: http://localhost:6006
# - API Gateway: http://localhost:6004
```

### Seçenek 2: .NET CLI ile Manuel Çalıştırma

```bash
# Identity Server'ı başlat
cd src/Services/Identity/Identity.API
dotnet run

# Ayrı terminal'de Shopping.Web'i başlat
cd src/WebApps/Shopping.Web
dotnet run
```

## 🔧 Gerekli Araçlar

### Minimum Gereksinimler:

1. **Docker Desktop** (en kolay seçenek)

   - [İndir: https://www.docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop)

2. **.NET 8.0 SDK** (manuel çalıştırma için)

   - [İndir: https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

3. **SQL Server** (Docker ile otomatik gelir)
   - Docker compose ile SQL Server otomatik olarak başlar

## 🐛 Sorun Giderme

### Sorun 1: "dotnet: not found"

**Çözüm:** .NET 8.0 SDK'yı yükleyin veya Docker kullanın

### Sorun 2: "docker-compose: not found"

**Çözüm:** Docker Desktop'ı yükleyin

### Sorun 3: Login sayfası açılmıyor

**Çözüm:**

- Identity Server'ın http://localhost:6006 adresinde çalıştığını kontrol edin
- Shopping.Web'in appsettings.json dosyasında IdentityServer.BaseUrl'in doğru olduğunu kontrol edin

### Sorun 4: Authentication çalışmıyor

**Çözüm:**

- Identity Server'da client yapılandırmasını kontrol edin
- Redirect URI'ların doğru olduğunu kontrol edin
- Browser cookie'lerini temizleyin

## 🎯 Test Kullanıcı Hesabı

Sistem otomatik olarak bir demo kullanıcı oluşturur:

- **Email:** admin@toyshop.com
- **Password:** Admin123!

## 📊 Port Yapılandırması

| Servis          | Port | URL                   |
| --------------- | ---- | --------------------- |
| Shopping Web    | 6005 | http://localhost:6005 |
| Identity Server | 6006 | http://localhost:6006 |
| API Gateway     | 6004 | http://localhost:6004 |
| Catalog API     | 6000 | http://localhost:6000 |
| Basket API      | 6001 | http://localhost:6001 |
| Discount gRPC   | 6002 | http://localhost:6002 |
| Ordering API    | 6003 | http://localhost:6003 |

## 🔄 Kimlik Doğrulama Akışı

1. Kullanıcı Shopping.Web'de korumalı bir sayfaya erişmeye çalışır
2. Sistem kullanıcıyı Identity Server'a (port 6006) yönlendirir
3. Kullanıcı giriş yapar (admin@toyshop.com / Admin123!)
4. Identity Server kullanıcıyı Shopping.Web'e geri yönlendirir
5. Shopping.Web artık API'lere erişim token'ı ile istekte bulunabilir

## 📝 Önemli Dosyalar

- `src/Services/Identity/Identity.API/Program.cs` - Identity Server yapılandırması
- `src/Services/Identity/Identity.API/Configuration/Config.cs` - Client ve scope tanımları
- `src/WebApps/Shopping.Web/Program.cs` - Authentication yapılandırması
- `src/docker-compose.yml` - Docker servisleri

## 🔍 Debug İpuçları

1. **Identity Server Logs:** `docker logs identity.api`
2. **Shopping Web Logs:** `docker logs shopping.web`
3. **Database Bağlantısı:** `docker logs identitydb`
4. **Network Connectivity:** `docker network ls` ve `docker network inspect`

Bu kılavuz ile Identity Server sistemi tamamen çalışır durumda olmalıdır! 🎉
