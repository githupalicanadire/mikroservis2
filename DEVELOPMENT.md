# EShop Microservices - Development Setup

## 🚨 Important Notice

This is a **.NET Core 8.0 microservices project** that requires specific tools for proper development. The current Node.js development server is a **compatibility layer** that provides project information and basic functionality.

## 🛠️ Required Tools

For full development experience, you need:

1. **[.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**
2. **[Docker Desktop](https://www.docker.com/products/docker-desktop)**
3. **[Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)** (recommended) or **[VS Code](https://code.visualstudio.com/)**

## 🚀 Quick Start Options

### Option 1: Docker (Recommended)

```bash
# Start all microservices
cd src
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

# Access the application
# Shopping Web UI: http://localhost:6005
# API Gateway: http://localhost:6004
# RabbitMQ Dashboard: http://localhost:15672 (guest/guest)
```

### Option 2: .NET CLI (Individual Services)

```bash
# Restore packages
cd src
dotnet restore

# Run the web application only
cd WebApps/Shopping.Web
dotnet run
# Access at: http://localhost:5000 or https://localhost:5001
```

### Option 3: Visual Studio 2022

1. Open `src/eshop-microservices.sln` in Visual Studio
2. Set `docker-compose` as the startup project
3. Press F5 to run with debugging

## 🏗️ Microservices Architecture

| Service        | Purpose          | Port | Technology                |
| -------------- | ---------------- | ---- | ------------------------- |
| Shopping.Web   | Frontend Web App | 6005 | ASP.NET Core MVC          |
| YarpApiGateway | API Gateway      | 6004 | YARP Reverse Proxy        |
| Catalog.API    | Product Catalog  | 6000 | ASP.NET Core Minimal APIs |
| Basket.API     | Shopping Cart    | 6001 | ASP.NET Core Web API      |
| Discount.Grpc  | Discount Service | 6002 | gRPC                      |
| Ordering.API   | Order Management | 6003 | ASP.NET Core Web API      |

## 🗄️ Databases & Infrastructure

| Component            | Port        | Credentials       | Purpose           |
| -------------------- | ----------- | ----------------- | ----------------- |
| PostgreSQL (Catalog) | 5432        | postgres/postgres | Product data      |
| PostgreSQL (Basket)  | 5433        | postgres/postgres | Cart data         |
| SQL Server (Orders)  | 1433        | sa/SwN12345678    | Order data        |
| Redis                | 6379        | -                 | Distributed cache |
| RabbitMQ             | 5672, 15672 | guest/guest       | Message broker    |

## 💻 Development Commands

### NPM Scripts (Current Environment)

```bash
npm run dev          # Start info server
npm run setup        # Setup confirmation
npm run info         # Project information
npm run docker:up    # Start Docker services (if Docker available)
npm run docker:down  # Stop Docker services (if Docker available)
```

### .NET Commands (With .NET SDK)

```bash
dotnet restore       # Restore packages
dotnet build         # Build solution
dotnet test          # Run tests
dotnet run           # Run specific project
```

## 🔧 Current Development Server

The current Node.js server provides:

- ✅ Project information and documentation
- ✅ Architecture overview
- ✅ Setup instructions
- ✅ Static file serving (limited)
- ❌ Full .NET application functionality
- ❌ Microservices communication
- ❌ Database operations

## 🚀 Moving to Full Development

To get the complete development experience:

1. **Install .NET 8.0 SDK**
2. **Install Docker Desktop**
3. **Clone the repository** to a machine with these tools
4. **Use one of the recommended startup options above**

## 📚 Learn More

- 📖 [Medium Article](https://medium.com/@mehmetozkaya/net-8-microservices-ddd-cqrs-vertical-clean-architecture-2dd7ebaaf4bd)
- 🎓 [Udemy Course](https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/)
- 💻 [GitHub Repository](https://github.com/aspnetrun/run-aspnetcore-microservices)

## 🤝 Architecture Patterns

This project demonstrates:

- **Domain-Driven Design (DDD)**
- **Command Query Responsibility Segregation (CQRS)**
- **Clean Architecture**
- **Event-Driven Communication**
- **Microservices Patterns**
- **API Gateway Pattern**
- **Database per Service**
- **Event Sourcing (with RabbitMQ)**

---

_For questions or issues, refer to the original repository documentation or the educational resources linked above._
