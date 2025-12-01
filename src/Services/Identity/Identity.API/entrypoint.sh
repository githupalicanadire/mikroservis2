#!/bin/bash
set -e

echo "Starting Identity API..."
echo "Waiting for SQL Server to be ready..."

# Wait for SQL Server to be ready
for i in {1..30}; do
    if timeout 1 bash -c "echo > /dev/tcp/identitydb/1433" 2>/dev/null; then
        echo "SQL Server is ready!"
        break
    fi
    echo "Waiting for SQL Server... (attempt $i/30)"
    sleep 2
done

echo "Starting .NET application..."
exec "$@"
