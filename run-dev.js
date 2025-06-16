const { spawn } = require("child_process");
const http = require("http");
const path = require("path");

console.log("🚀 Starting EShop Microservices Development Environment");

// Check if we should run Docker or .NET CLI
const runDocker =
  process.env.RUN_DOCKER === "true" || process.argv.includes("--docker");

if (runDocker) {
  console.log("🐳 Starting with Docker Compose...");

  // Change to src directory and run docker-compose
  const dockerProcess = spawn(
    "docker-compose",
    [
      "-f",
      "docker-compose.yml",
      "-f",
      "docker-compose.override.yml",
      "up",
      "-d",
    ],
    {
      cwd: path.join(__dirname, "src"),
      stdio: "inherit",
      shell: true,
    },
  );

  dockerProcess.on("close", (code) => {
    if (code === 0) {
      console.log("✅ Docker services started successfully!");
      console.log("🌐 Shopping Web: http://localhost:6005");
      console.log("🔐 Identity Server: http://localhost:6006");
      console.log("🚪 API Gateway: http://localhost:6004");
    } else {
      console.log(`❌ Docker process exited with code ${code}`);
    }
  });

  dockerProcess.on("error", (err) => {
    console.log("❌ Failed to start Docker:", err.message);
    console.log("💡 Make sure Docker Desktop is installed and running");

    // Fallback to information server
    startInfoServer();
  });
} else {
  console.log("🔧 Starting .NET development server...");

  // Try to run the .NET application
  const dotnetProcess = spawn("dotnet", ["run"], {
    cwd: path.join(__dirname, "src/WebApps/Shopping.Web"),
    stdio: "inherit",
    shell: true,
  });

  dotnetProcess.on("close", (code) => {
    if (code === 0) {
      console.log("✅ .NET application started successfully!");
    } else {
      console.log(`❌ .NET process exited with code ${code}`);
    }
  });

  dotnetProcess.on("error", (err) => {
    console.log("❌ Failed to start .NET application:", err.message);
    console.log("💡 Make sure .NET 8.0 SDK is installed");

    // Fallback to information server
    startInfoServer();
  });
}

function startInfoServer() {
  console.log("📋 Starting development information server...");
  require("./server.js");
}

// Handle graceful shutdown
process.on("SIGTERM", () => {
  console.log("🛑 Shutting down development server...");
  process.exit(0);
});

process.on("SIGINT", () => {
  console.log("🛑 Shutting down development server...");
  process.exit(0);
});
