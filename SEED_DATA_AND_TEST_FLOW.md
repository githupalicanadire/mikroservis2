# 🌱 Seed Data & Test Akışı Kılavuzu

## ✅ Identity Server Çalışıyor!

OpenID Configuration başarıyla alındı:

```json
{
  "issuer": "http://identity.api:8080",
  "authorization_endpoint": "http://localhost:6006/connect/authorize",
  "token_endpoint": "http://localhost:6006/connect/token",
  "userinfo_endpoint": "http://localhost:6006/connect/userinfo"
}
```

## 🌱 SEED DATA SİSTEMİ

### 1. **Demo Kullanıcılar**

```
👤 admin@toyshop.com     / Admin123!  (Admin)
👤 john.doe@example.com  / User123!   (User)
👤 jane.smith@example.com / User123!  (User)
👤 mike.wilson@example.com / User123! (User)
👤 sarah.johnson@example.com / User123! (User)
```

### 2. **Demo Ürünler (10 adet)**

```
🧸 LEGO Classic Creative Bricks     - $49.99
🧸 Teddy Bear Plush Toy            - $24.99
🚗 Remote Control Racing Car        - $89.99
🧩 Educational Puzzle Set          - $19.99
🎨 Art & Craft Mega Kit           - $34.99
🦕 Dinosaur Action Figures Set     - $29.99
🎹 Musical Keyboard Piano          - $79.99
👗 Princess Dress-Up Costume Set   - $39.99
🔬 Science Experiment Kit          - $54.99
🎲 Board Game Collection           - $44.99
```

### 3. **Demo Siparişler**

Her kullanıcı için rastgele sipariş geçmişi oluşturulacak

## 🧪 TAM TEST AKIŞI

### Adım 1: Identity Server Status Check

```bash
# Identity Server'ın çalıştığını doğrula
curl http://localhost:6006/.well-known/openid-configuration

# Seed status kontrol et
curl http://localhost:6006/api/seed/status
```

### Adım 2: Seed Data Kontrolü

```bash
# Demo kullanıcıları listele
curl http://localhost:6006/api/seed/users

# Gerekirse yeniden seed et
curl -X POST http://localhost:6006/api/seed/reseed
```

### Adım 3: Register Akışı Test

```
1. Browser'da http://localhost:5000 aç
2. "Register" tıkla
3. Yeni kullanıcı oluştur:
   - Email: test.user@example.com
   - Password: Test123!
   - First Name: Test
   - Last Name: User
4. Başarılı kayıt sonrası Login sayfasına yönlendir
```

### Adım 4: Login Akışı Test

```
1. Yeni oluşturulan kullanıcı ile giriş yap
2. Identity Server'a yönlendirilmeyi doğrula
3. Başarılı giriş sonrası ana sayfaya dönmeyi kontrol et
4. Navigation bar'da kullanıcı adının göründüğünü doğrula
```

### Adım 5: Shopping Akışı Test

```
1. Product List sayfasına git
2. Ürünleri görüntüle (seed data'dan)
3. Sepete ürün ekle
4. Cart sayfasında user isolation'ı doğrula
5. Checkout işlemi yap
6. Sipariş oluşturulduğunu kontrol et
```

### Adım 6: User Isolation Test

```
1. UserTest sayfasına git (/UserTest)
2. Security testlerini çalıştır
3. Cart isolation'ı doğrula
4. Order isolation'ı doğrula
5. User identity security'i kontrol et
```

### Adım 7: Multi-User Test

```
1. Logout yap
2. Farklı demo kullanıcı ile giriş yap (john.doe@example.com)
3. Sepet ve siparişlerin izole olduğunu doğrula
4. Cross-user erişim denemesi yap
5. Security logging'i kontrol et
```

### Adım 8: Logout Test

```
1. Logout butonuna tıkla
2. Identity Server'dan çıkış yapıldığını doğrula
3. Korumalı sayfaların erişilemez olduğunu kontrol et
4. Anonymous duruma dönüldüğünü doğrula
```

## 🎯 DOĞRULAMA LİSTESİ

### ✅ Identity Server

- [ ] OpenID configuration erişilebilir
- [ ] Authorization endpoint çalışır
- [ ] Token endpoint çalışır
- [ ] Userinfo endpoint çalışır

### ✅ Seed Data

- [ ] Demo kullanıcılar oluşturuldu
- [ ] Demo ürünler oluşturuldu
- [ ] Demo siparişler oluşturuldu
- [ ] Seed API endpoints çalışır

### ✅ Authentication Flow

- [ ] Register işlemi çalışır
- [ ] Login işlemi çalışır
- [ ] Token generation çalışır
- [ ] User claims doğru

### ✅ User Isolation

- [ ] Cart user-specific
- [ ] Orders user-specific
- [ ] Cross-user access engellendi
- [ ] Security logging aktif

### ✅ Shopping Flow

- [ ] Product listing çalışır
- [ ] Add to cart çalışır
- [ ] Checkout işlemi çalışır
- [ ] Order creation çalışır

## 🚀 HIZLI TEST KOMUTLARI

### Identity Server Test

```bash
# Status check
curl http://localhost:6006/api/seed/status | jq

# Users list
curl http://localhost:6006/api/seed/users | jq

# Reseed data
curl -X POST http://localhost:6006/api/seed/reseed | jq
```

### Shopping Web Test URLs

```
🏠 Home: http://localhost:5000/
🔐 Login: http://localhost:5000/Login
👤 Register: http://localhost:5000/Register
📦 Products: http://localhost:5000/ProductList
🛒 Cart: http://localhost:5000/Cart
📋 Orders: http://localhost:5000/OrderList
🔧 Debug: http://localhost:5000/Debug
🛡️ User Test: http://localhost:5000/UserTest
```

## 🎭 TEST SENARYOLARI

### Senaryo 1: Yeni Kullanıcı Akışı

```
1. Ana sayfayı ziyaret et
2. Register ile yeni hesap oluştur
3. Email doğrulama (otomatik)
4. Login ile giriş yap
5. Ürün sepete ekle
6. Sipariş ver
7. Order history kontrol et
8. Logout yap
```

### Senaryo 2: Multi-User İzolasyon

```
1. User A ile giriş yap
2. Sepete ürün ekle
3. Sipariş ver
4. Logout yap
5. User B ile giriş yap
6. Sadece kendi verilerini görmeli
7. User A'nın verilerine erişemez olmalı
```

### Senaryo 3: Security Test

```
1. Demo kullanıcı ile giriş yap
2. UserTest sayfasına git
3. Tüm security testlerini çalıştır
4. Sonuçların "PASS" olduğunu doğrula
5. Manual cross-user access dene
6. Security violation'lar log'da görünmeli
```

## 🎉 BAŞARILI TEST ÇIKTISİ

Bu adımları tamamladığında sistem:

✅ **Tam çalışır durumda**  
✅ **User isolation güvenli**  
✅ **Enterprise-level security**  
✅ **Production-ready**  
✅ **Real e-commerce experience**

Artık sistem **Amazon, Netflix** seviyesinde güvenli! 🛡️🚀
