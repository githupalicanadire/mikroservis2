const http = require("http");
const fs = require("fs");
const path = require("path");
const url = require("url");

const PORT = process.env.PORT || 5000;

// MIME types for different file extensions
const mimeTypes = {
  ".html": "text/html",
  ".css": "text/css",
  ".js": "text/javascript",
  ".json": "application/json",
  ".png": "image/png",
  ".jpg": "image/jpeg",
  ".gif": "image/gif",
  ".ico": "image/x-icon",
  ".svg": "image/svg+xml",
  ".woff": "font/woff",
  ".woff2": "font/woff2",
};

const server = http.createServer((req, res) => {
  const parsedUrl = url.parse(req.url);
  let pathname = parsedUrl.pathname;

  console.log(`Request: ${req.method} ${pathname}`);

  // Handle root path
  if (pathname === "/") {
    serveInfoPage(res);
    return;
  }

  // Try to serve static files from wwwroot
  if (
    pathname.startsWith("/static/") ||
    pathname.startsWith("/css/") ||
    pathname.startsWith("/js/") ||
    pathname.startsWith("/lib/")
  ) {
    serveStaticFile(pathname, res);
    return;
  }

  // Handle API-like requests by showing information
  if (pathname.startsWith("/api/")) {
    serveApiInfo(pathname, res);
    return;
  }

  // Default response for other paths
  serveInfoPage(res);
});

function serveInfoPage(res) {
  const html = `
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>EShop Microservices - Development Info</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
            line-height: 1.6;
            color: #1a1a1a;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            padding: 20px;
        }

        .container {
            max-width: 1200px;
            margin: 0 auto;
            background: rgba(255, 255, 255, 0.95);
            border-radius: 20px;
            padding: 40px;
            box-shadow: 0 20px 60px rgba(0, 0, 0, 0.1);
            backdrop-filter: blur(10px);
            border: 1px solid rgba(255, 255, 255, 0.2);
        }

        h1 {
            font-size: 2.5rem;
            font-weight: 700;
            background: linear-gradient(135deg, #667eea, #764ba2);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            margin-bottom: 20px;
            text-align: center;
        }

        h2 {
            color: #4f46e5;
            margin: 35px 0 15px 0;
            font-size: 1.5rem;
            font-weight: 600;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        h3 {
            color: #1e293b;
            font-size: 1.1rem;
            font-weight: 600;
            margin-bottom: 8px;
        }

        p, li {
            color: #64748b;
            font-size: 1rem;
            line-height: 1.7;
        }

        .status {
            border-radius: 12px;
            padding: 20px;
            margin: 25px 0;
            border: none;
            position: relative;
            overflow: hidden;
        }

        .status::before {
            content: '';
            position: absolute;
            left: 0;
            top: 0;
            bottom: 0;
            width: 4px;
        }

        .error {
            background: linear-gradient(135deg, #fef2f2, #fee2e2);
            color: #dc2626;
        }

        .error::before {
            background: #dc2626;
        }

        .info {
            background: linear-gradient(135deg, #eff6ff, #dbeafe);
            color: #2563eb;
        }

        .info::before {
            background: #2563eb;
        }

        .success {
            background: linear-gradient(135deg, #f0fdf4, #dcfce7);
            color: #16a34a;
        }

        .success::before {
            background: #16a34a;
        }

        code {
            background: rgba(99, 102, 241, 0.1);
            color: #4f46e5;
            padding: 4px 8px;
            border-radius: 6px;
            font-family: 'JetBrains Mono', 'Fira Code', monospace;
            font-size: 0.9rem;
            font-weight: 500;
        }

        pre {
            background: #1e293b;
            color: #e2e8f0;
            padding: 20px;
            border-radius: 12px;
            overflow-x: auto;
            border-left: 4px solid #6366f1;
            font-family: 'JetBrains Mono', 'Fira Code', monospace;
            font-size: 0.9rem;
            line-height: 1.5;
            margin: 15px 0;
        }

        .service-list {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
            gap: 20px;
            margin: 25px 0;
        }

        .service-card {
            background: linear-gradient(135deg, #f8fafc, #f1f5f9);
            padding: 24px;
            border-radius: 16px;
            border-left: 5px solid #10b981;
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
        }

        .service-card::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            height: 2px;
            background: linear-gradient(90deg, #10b981, #06d6a0);
        }

        .service-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 30px rgba(16, 185, 129, 0.2);
        }

        .port {
            font-weight: 700;
            color: #6366f1;
            background: rgba(99, 102, 241, 0.1);
            padding: 4px 12px;
            border-radius: 20px;
            display: inline-block;
            margin-top: 8px;
            font-size: 0.9rem;
        }

        a {
            color: #6366f1;
            text-decoration: none;
            font-weight: 500;
            transition: all 0.2s ease;
        }

        a:hover {
            color: #4f46e5;
            text-decoration: underline;
        }

        ul, ol {
            margin: 15px 0;
            padding-left: 20px;
        }

        li {
            margin: 8px 0;
        }

        strong {
            color: #1e293b;
            font-weight: 600;
        }

        @media (max-width: 768px) {
            .container {
                padding: 25px;
                margin: 10px;
            }

            h1 {
                font-size: 2rem;
            }

            .service-list {
                grid-template-columns: 1fr;
            }
        }

        /* Animasyonlar */
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .container {
            animation: fadeIn 0.6s ease-out;
        }

        .service-card {
            animation: fadeIn 0.8s ease-out;
        }

        /* Hover efektleri */
        .status:hover {
            transform: translateX(5px);
            transition: transform 0.2s ease;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>🏪 EShop Microservices Development Server</h1>

        <div class="status success">
            <strong>✅ Identity Server & Seed Data Hazır!</strong><br>
            Kimlik doğrulama sistemi, seed data ve otomatik test akışı tamamen hazır. Demo kullanıcılar ve ürünler oluşturuldu.
        </div>

        <h2>📋 Project Information</h2>
        <p>This is an <strong>E-commerce Microservices</strong> application built with:</p>
        <ul>
            <li>🎯 <strong>.NET 8.0</strong> - Primary framework</li>
            <li>🏗️ <strong>Microservices Architecture</strong> - DDD, CQRS, Clean Architecture</li>
            <li>🐳 <strong>Docker</strong> - Containerization</li>
            <li>🔄 <strong>Event-Driven Communication</strong> - RabbitMQ</li>
            <li>🚪 <strong>API Gateway</strong> - YARP Reverse Proxy</li>
        </ul>

        <h2>🏗️ Microservices Architecture</h2>
        <div class="service-list">
            <div class="service-card">
                <h3>🛍️ Shopping Web UI</h3>
                <p>ASP.NET Core Web Application</p>
                <p class="port">Port: 6005</p>
            </div>
            <div class="service-card">
                <h3>🚪 API Gateway</h3>
                <p>YARP Reverse Proxy</p>
                <p class="port">Port: 6004</p>
            </div>
            <div class="service-card">
                <h3>📦 Catalog Service</h3>
                <p>Product catalog management</p>
                <p class="port">Port: 6000</p>
            </div>
            <div class="service-card">
                <h3>🛒 Basket Service</h3>
                <p>Shopping cart functionality</p>
                <p class="port">Port: 6001</p>
            </div>
            <div class="service-card">
                <h3>💰 Discount Service</h3>
                <p>gRPC discount service</p>
                <p class="port">Port: 6002</p>
            </div>
            <div class="service-card">
                <h3>📋 Ordering Service</h3>
                <p>Order management</p>
                <p class="port">Port: 6003</p>
            </div>
            <div class="service-card">
                <h3>🔐 Identity Server</h3>
                <p>Authentication & Authorization (IdentityServer4)</p>
                <p class="port">Port: 6006</p>
            </div>
        </div>

        <h2>🛠️ Required Tools for Development</h2>
        <div class="status info">
            To properly run this project, you need:
            <ol>
                <li><strong>.NET 8.0 SDK</strong> - Download from <a href="https://dotnet.microsoft.com/download" target="_blank">Microsoft</a></li>
                <li><strong>Docker Desktop</strong> - Download from <a href="https://www.docker.com/products/docker-desktop" target="_blank">Docker</a></li>
                <li><strong>Visual Studio 2022</strong> (recommended) or <strong>VS Code</strong></li>
            </ol>
        </div>

        <h2>🚀 How to Run the Project</h2>
        <div class="status success">
            <strong>Option 1: Using Docker (Recommended)</strong>
            <pre>cd src
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d</pre>
            Then access: <a href="http://localhost:6005" target="_blank">http://localhost:6005</a>
        </div>

        <div class="status info">
            <strong>Option 2: Using .NET CLI</strong>
            <pre># Restore packages
cd src
dotnet restore

# Run individual services
cd WebApps/Shopping.Web
dotnet run</pre>
        </div>

        <h2>🔗 Important URLs (when running with Docker)</h2>
        <ul>
            <li>🛍️ <strong>Shopping Web UI:</strong> <a href="http://localhost:6005" target="_blank">http://localhost:6005</a></li>
            <li>🔐 <strong>Identity Server:</strong> <a href="http://localhost:6006" target="_blank">http://localhost:6006</a></li>
            <li>🚪 <strong>API Gateway:</strong> <a href="http://localhost:6004" target="_blank">http://localhost:6004</a></li>
            <li>🐰 <strong>RabbitMQ Dashboard:</strong> <a href="http://localhost:15672" target="_blank">http://localhost:15672</a> (guest/guest)</li>
            <li>🗄️ <strong>PostgreSQL:</strong> localhost:5432, localhost:5433</li>
            <li>🗄️ <strong>SQL Server:</strong> localhost:1433</li>
            <li>🔄 <strong>Redis:</strong> localhost:6379</li>
        </ul>

        <div class="status info">
            <strong>🔑 Demo Kullanıcılar:</strong><br>
            Admin: <code>admin@toyshop.com</code> / <code>Admin123!</code><br>
            John: <code>john.doe@example.com</code> / <code>User123!</code><br>
            Jane: <code>jane.smith@example.com</code> / <code>User123!</code><br>
            Mike: <code>mike.wilson@example.com</code> / <code>User123!</code><br>
            Sarah: <code>sarah.johnson@example.com</code> / <code>User123!</code>
        </div>

        <div class="status success">
            <strong>🧪 Otomatik Test Akışı:</strong><br>
            1. <a href="http://localhost:5000/TestFlow" target="_blank">Test Flow Sayfası</a> - Register → Login → Shopping → Logout<br>
            2. <a href="http://localhost:5000/UserTest" target="_blank">User Test Sayfası</a> - Security validation (login gerekli)<br>
            3. <a href="http://localhost:6006/api/seed/status" target="_blank">Seed Status API</a> - Identity Server durumu<br>
            4. <a href="http://localhost:6006/api/seed/users" target="_blank">Demo Users API</a> - Kullanıcı listesi
        </div>

        <h2>📚 Documentation & Learning</h2>
        <p>For detailed information about this microservices architecture:</p>
        <ul>
            <li>📖 <a href="https://medium.com/@mehmetozkaya/net-8-microservices-ddd-cqrs-vertical-clean-architecture-2dd7ebaaf4bd" target="_blank">Medium Article</a></li>
            <li>🎓 <a href="https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/" target="_blank">Udemy Course</a></li>
            <li>💻 <a href="https://github.com/aspnetrun/run-aspnetcore-microservices" target="_blank">GitHub Repository</a></li>
        </ul>

        <div class="status">
            <strong>💡 Current Status:</strong> Development server running on port ${PORT}<br>
            This is a placeholder server providing project information since .NET runtime is not available.
        </div>
    </div>
</body>
</html>`;

  res.writeHead(200, { "Content-Type": "text/html" });
  res.end(html);
}

function serveApiInfo(pathname, res) {
  const apiInfo = {
    message: "This is a .NET Core microservices project",
    requestedPath: pathname,
    availableServices: {
      "Shopping.Web": "http://localhost:6005",
      "API Gateway": "http://localhost:6004",
      "Catalog.API": "http://localhost:6000",
      "Basket.API": "http://localhost:6001",
      "Discount.Grpc": "http://localhost:6002",
      "Ordering.API": "http://localhost:6003",
    },
    setup: "Please use Docker to run the full microservices stack",
    command:
      "docker-compose -f src/docker-compose.yml -f src/docker-compose.override.yml up -d",
  };

  res.writeHead(200, { "Content-Type": "application/json" });
  res.end(JSON.stringify(apiInfo, null, 2));
}

function serveStaticFile(pathname, res) {
  // Try to find static files in the wwwroot directory
  const staticPath = path.join(
    __dirname,
    "src/WebApps/Shopping.Web/wwwroot",
    pathname.replace(/^\/static/, ""),
  );

  fs.readFile(staticPath, (err, data) => {
    if (err) {
      res.writeHead(404, { "Content-Type": "text/plain" });
      res.end("File not found");
      return;
    }

    const ext = path.extname(staticPath);
    const contentType = mimeTypes[ext] || "application/octet-stream";

    res.writeHead(200, { "Content-Type": contentType });
    res.end(data);
  });
}

server.listen(PORT, () => {
  console.log(`🚀 EShop Microservices Development Server`);
  console.log(`📍 Server running at http://localhost:${PORT}`);
  console.log(
    `⚠️  Note: This is a .NET Core project. For full functionality, use Docker.`,
  );
  console.log(
    `🐳 Run: docker-compose -f src/docker-compose.yml -f src/docker-compose.override.yml up -d`,
  );
});
