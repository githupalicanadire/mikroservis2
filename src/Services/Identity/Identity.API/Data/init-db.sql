-- Create IdentityDb database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'IdentityDb')
BEGIN
    CREATE DATABASE IdentityDb;
    PRINT 'IdentityDb database created successfully';
END
ELSE
BEGIN
    PRINT 'IdentityDb database already exists';
END
GO

USE IdentityDb;
GO

-- The tables will be created by Entity Framework when the application starts
PRINT 'Database initialization complete';
GO
