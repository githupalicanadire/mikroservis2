# ===================================================================
# .NET 8 Mikroservis E-Ticaret Projesi - Otomatik Kurulum Scripti
# PowerShell ile SSL, Migration ve Docker kurulumu
# ===================================================================

Write-Host "=== .NET 8 Mikroservis E-Ticaret Projesi Kurulum Basliyor ===" -ForegroundColor Green

# Admin yetkisi kontrolu
function Test-Administrator {
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
    $principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
    return $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

if (-not (Test-Administrator)) {
    Write-Host "Bu script yonetici (Administrator) yetkisiyle calistirilmalidir!" -ForegroundColor Red
    Write-Host "PowerShell'i 'Yonetici olarak calistir' secenegiyle acin." -ForegroundColor Yellow
    Read-Host "Devam etmek icin Enter'a basin..."
    exit 1
}

# Gerekli yazilimlari kontrol etme fonksiyonu
function Test-Prerequisites {
    Write-Host "`nGerekli yazilimlar kontrol ediliyor..." -ForegroundColor Cyan
    
    # .NET 8 kontrolu
    try {
        $dotnetVersion = dotnet --version
        Write-Host ".NET SDK: $dotnetVersion" -ForegroundColor Green
        
        if ([version]$dotnetVersion -lt [version]"8.0.0") {
            throw ".NET 8.0 veya uzeri gerekli!"
        }
    }
    catch {
        Write-Host ".NET 8 SDK bulunamadi!" -ForegroundColor Red
        Write-Host "https://dotnet.microsoft.com/download/dotnet/8.0 adresinden indirin." -ForegroundColor Yellow
        return $false
    }
    
    # Docker kontrolu
    try {
        $dockerVersion = docker --version
        Write-Host "Docker: $dockerVersion" -ForegroundColor Green
    }
    catch {
        Write-Host "Docker bulunamadi!" -ForegroundColor Red
        Write-Host "https://www.docker.com/products/docker-desktop adresinden indirin." -ForegroundColor Yellow
        return $false
    }
    
    # Docker Compose kontrolu
    try {
        $dockerComposeVersion = docker-compose --version
        Write-Host "Docker Compose: $dockerComposeVersion" -ForegroundColor Green
    }
    catch {
        Write-Host "Docker Compose bulunamadi!" -ForegroundColor Red
        return $false
    }
    
    return $true
}

# SSL Sertifikalarini duzeltme fonksiyonu
function Fix-SSLCertificates {
    Write-Host "`nSSL Sertifikalari duzeltiliyor..." -ForegroundColor Cyan
    
    try {
        # .NET HTTPS gelistirme sertifikasini temizle ve yeniden olustur
        Write-Host "Mevcut sertifikalar temizleniyor..." -ForegroundColor Yellow
        dotnet dev-certs https --clean
        
        Write-Host "Yeni HTTPS sertifikasi olusturuluyor..." -ForegroundColor Yellow
        dotnet dev-certs https --trust
        
        # Manuel olarak guvenilen sertifikayi olustur
        dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx" -p "SecurePwd123!"
        
        Write-Host "SSL sertifikalari basariyla duzeltildi!" -ForegroundColor Green
    }
    catch {
        Write-Host "SSL sertifika olusturma hatasi: $($_.Exception.Message)" -ForegroundColor Red
        Write-Host "Manuel olarak 'dotnet dev-certs https --trust' calistirin." -ForegroundColor Yellow
    }
}

# Docker servislerini durdurma fonksiyonu
function Stop-ExistingServices {
    Write-Host "`nMevcut Docker servisleri durduruluyor..." -ForegroundColor Cyan
    
    # Mevcut container'lari durdur ve temizle
    docker-compose -f src/docker-compose.yml -f src/docker-compose.override.yml down --remove-orphans 2>$null
    
    # Kullanilan portlari temizle
    $portsToCheck = @(5432, 5433, 1433, 6379, 5672, 15672, 6000, 6001, 6002, 6003, 6004, 6005)
    
    foreach ($port in $portsToCheck) {
        $process = Get-NetTCPConnection -LocalPort $port -ErrorAction SilentlyContinue
        if ($process) {
            Write-Host "Port $port kullanimda, temizleniyor..." -ForegroundColor Yellow
            Stop-Process -Id $process.OwningProcess -Force -ErrorAction SilentlyContinue
        }
    }
}

# Database Migration fonksiyonu
function Run-DatabaseMigrations {
    Write-Host "`nVeritabani migration'lari calistiriliyor..." -ForegroundColor Cyan
    
    # Ordering Service migration'i
    Push-Location "src/Services/Ordering/Ordering.API"
    try {
        Write-Host "Ordering Service migration'i..." -ForegroundColor Yellow
        
        # EF Core tools kurulu mu kontrol et
        dotnet tool install --global dotnet-ef --ignore-failed-sources 2>$null
        
        # Migration'lari uygula (container ayakta olduktan sonra)
        Write-Host "Migration'lar container ayakta olduktan sonra uygulanacak..." -ForegroundColor Yellow
    }
    catch {
        Write-Host "Migration hazirliginda hata: $($_.Exception.Message)" -ForegroundColor Red
    }
    finally {
        Pop-Location
    }
}

# Docker servisleri baslatma fonksiyonu
function Start-DockerServices {
    Write-Host "`nDocker servisleri baslatiliyor..." -ForegroundColor Cyan
    
    # src dizinine gec
    if (-not (Test-Path "src")) {
        Write-Host "src dizini bulunamadi! Proje ana dizininde oldugunuzdan emin olun." -ForegroundColor Red
        return $false
    }
    
    Push-Location "src"
    
    try {
        # Docker compose ile servisleri baslat
        Write-Host "Docker Compose baslatiliyor..." -ForegroundColor Yellow
        docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "Docker servisleri basariyla baslatildi!" -ForegroundColor Green
            return $true
        } else {
            Write-Host "Docker servisleri baslatilamadi!" -ForegroundColor Red
            return $false
        }
    }
    catch {
        Write-Host "Docker Compose hatasi: $($_.Exception.Message)" -ForegroundColor Red
        return $false
    }
    finally {
        Pop-Location
    }
}

# Servislerin hazir olmasini bekleme fonksiyonu
function Wait-ForServices {
    Write-Host "`nServisler baslatilirken bekleniyor..." -ForegroundColor Cyan
    
    $maxWaitTime = 300 # 5 dakika
    $waitTime = 0
    $checkInterval = 10
    
    $services = @(
        @{Name="PostgreSQL Catalog"; Port=5432},
        @{Name="PostgreSQL Basket"; Port=5433},
        @{Name="SQL Server"; Port=1433},
        @{Name="Redis"; Port=6379},
        @{Name="RabbitMQ"; Port=5672},
        @{Name="Catalog API"; Port=6000},
        @{Name="Basket API"; Port=6001},
        @{Name="Discount gRPC"; Port=6002},
        @{Name="Ordering API"; Port=6003},
        @{Name="YARP Gateway"; Port=6004},
        @{Name="Shopping Web"; Port=6005}
    )
    
    while ($waitTime -lt $maxWaitTime) {
        $allReady = $true
        
        foreach ($service in $services) {
            try {
                $connection = Test-NetConnection -ComputerName localhost -Port $service.Port -WarningAction SilentlyContinue
                if (-not $connection.TcpTestSucceeded) {
                    $allReady = $false
                    break
                }
            }
            catch {
                $allReady = $false
                break
            }
        }
        
        if ($allReady) {
            Write-Host "Tum servisler hazir!" -ForegroundColor Green
            return $true
        }
        
        Write-Host "Servisler hala baslatiliyor... ($waitTime/$maxWaitTime saniye)" -ForegroundColor Yellow
        Start-Sleep $checkInterval
        $waitTime += $checkInterval
    }
    
    Write-Host "Bazi servisler belirtilen surede baslatilamadi." -ForegroundColor Red
    return $false
}

# Migration'lari uygulama fonksiyonu
function Apply-DatabaseMigrations {
    Write-Host "`nVeritabani migration'lari uygulanıyor..." -ForegroundColor Cyan
    
    Push-Location "src/Services/Ordering/Ordering.API"
    try {
        # Entity Framework migration'ini uygula
        Write-Host "Ordering Database migration'i uygulaniyor..." -ForegroundColor Yellow
        
        # Connection string'i test et
        $connectionString = "Server=localhost,1433;Database=OrderDb;User Id=sa;Password=SwN12345678;Encrypt=False;TrustServerCertificate=True"
        
        # Migration'i uygula
        $env:ConnectionStrings__Database = $connectionString
        dotnet ef database update --verbose
        
        Write-Host "Database migration'i basariyla uygulandi!" -ForegroundColor Green
    }
    catch {
        Write-Host "Migration hatasi: $($_.Exception.Message)" -ForegroundColor Red
        Write-Host "Manuel olarak 'dotnet ef database update' calistirin." -ForegroundColor Yellow
    }
    finally {
        Pop-Location
    }
}

# Container durumunu kontrol etme fonksiyonu
function Check-ContainerStatus {
    Write-Host "`nContainer durumlari kontrol ediliyor..." -ForegroundColor Cyan
    
    $containers = docker ps --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"
    Write-Host $containers -ForegroundColor White
    
    # Health check'leri kontrol et
    $urls = @(
        "http://localhost:6000/health",
        "http://localhost:6001/health", 
        "http://localhost:6003/health",
        "http://localhost:6004/health"
    )
    
    Write-Host "`nHealth Check'ler kontrol ediliyor..." -ForegroundColor Cyan
    foreach ($url in $urls) {
        try {
            $response = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec 5
            if ($response.StatusCode -eq 200) {
                Write-Host "$url - Healthy" -ForegroundColor Green
            }
        }
        catch {
            Write-Host "$url - Unhealthy" -ForegroundColor Red
        }
    }
}

# Log'lari gosterme fonksiyonu
function Show-ServiceLogs {
    Write-Host "`nSon 50 log satiri gosteriliyor..." -ForegroundColor Cyan
    
    Push-Location "src"
    try {
        docker-compose logs --tail=50
    }
    finally {
        Pop-Location
    }
}

# Ana kurulum fonksiyonu
function Start-Setup {
    # Header
    Write-Host @"
================================================================
              .NET 8 MIKROSERVIS E-TICARET                   
                   OTOMATIK KURULUM                           
================================================================
"@ -ForegroundColor Cyan

    # Gereksinimler kontrolu
    if (-not (Test-Prerequisites)) {
        Write-Host "`nGerekli yazilimlar eksik! Kurulumu tamamlayip tekrar deneyin." -ForegroundColor Red
        Read-Host "Cikmak icin Enter'a basin..."
        return
    }
    
    # SSL sertifikalarini duzelt
    Fix-SSLCertificates
    
    # Mevcut servisleri durdur
    Stop-ExistingServices
    
    # Migration hazirligi
    Run-DatabaseMigrations
    
    # Docker servislerini baslat
    if (-not (Start-DockerServices)) {
        Write-Host "`nDocker servisleri baslatilamadi!" -ForegroundColor Red
        Read-Host "Cikmak icin Enter'a basin..."
        return
    }
    
    # Servislerin hazir olmasini bekle
    Wait-ForServices
    
    # Migration'lari uygula
    Start-Sleep 30 # Database'lerin tamamen hazir olmasi icin bekle
    Apply-DatabaseMigrations
    
    # Container durumlarini kontrol et
    Check-ContainerStatus
    
    # Basari mesaji
    Write-Host @"

=============================================================== 
                          KURULUM TAMAMLANDI!                       
===============================================================

UYGULAMALARA ERISIM:
---------------------------------------------------------------
Shopping Web UI:    https://localhost:6065                 
API Gateway:        https://localhost:6064                 
Catalog API:        https://localhost:6060                 
Basket API:         https://localhost:6061                 
Discount gRPC:      https://localhost:6062                 
Ordering API:       https://localhost:6063                 
RabbitMQ Management: http://localhost:15672 (guest/guest)   
---------------------------------------------------------------

YONETIM KOMUTLARI:
• Container'lari durdur:  docker-compose -f src/docker-compose.yml down
• Log'lari gor:           docker-compose -f src/docker-compose.yml logs
• Yeniden baslat:         docker-compose -f src/docker-compose.yml restart

"@ -ForegroundColor Green

    # Tarayiciyi ac
    $openBrowser = Read-Host "Shopping Web UI'i tarayicida acmak ister misiniz? (y/n)"
    if ($openBrowser -eq 'y' -or $openBrowser -eq 'Y' -or $openBrowser -eq '') {
        Start-Process "https://localhost:6065"
    }
    
    # Log'lari goster secenegi
    $showLogs = Read-Host "`nSon log'lari gormek ister misiniz? (y/n)"
    if ($showLogs -eq 'y' -or $showLogs -eq 'Y') {
        Show-ServiceLogs
    }
}

# Hata yonetimi
function Handle-Error {
    param($ErrorMessage)
    
    Write-Host "`nHATA OLUSTU!" -ForegroundColor Red
    Write-Host "Hata Detayi: $ErrorMessage" -ForegroundColor Yellow
    
    Write-Host "`nSORUN GIDERME:" -ForegroundColor Cyan
    Write-Host "1. Docker Desktop'in calistiginden emin olun" -ForegroundColor White
    Write-Host "2. Yonetici yetkisiyle calistirdiginizdan emin olun" -ForegroundColor White
    Write-Host "3. Antivirus yaziliminin Docker'i engellememesini saglayin" -ForegroundColor White
    Write-Host "4. Port'larin baska uygulamalar tarafindan kullanilmadigini kontrol edin" -ForegroundColor White
    
    Show-ServiceLogs
    
    Read-Host "`nCikmak icin Enter'a basin..."
}

# Script baslangiç
try {
    Start-Setup
}
catch {
    Handle-Error $_.Exception.Message
}

Write-Host "`nScript tamamlandi. Iyi calismalar!" -ForegroundColor Green
Read-Host "Cikmak icin Enter'a basin..."
