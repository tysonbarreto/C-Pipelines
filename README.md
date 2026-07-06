# Customer Sync Platform

A .NET-based platform for syncing and managing customer data with support for regional grouping and reporting.

## Project Structure

The solution follows a clean architecture pattern with the following projects:

- **CustomerSync.Domain** - Core business entities and domain logic
- **CustomerSync.Application** - Application services and business logic
- **CustomerSync.Infrastructure** - External service integrations
- **CustomerSync.Persistence** - Data persistence layer
- **CustomerSync.Api** - REST API endpoints
- **CustomerSync.Worker** - Background worker service for processing tasks
- **CustomerSync.UnitTests** - Unit tests for the application

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later
- Visual Studio Code or Visual Studio

### Building the Project

```bash
dotnet build
```

### Running the Worker

```bash
dotnet run --project CustomerSync.Worker
```

### Running Tests

```bash
dotnet test
```

## Features

- In-memory and API-based customer data providers
- Customer grouping by region
- CSV and JSON report generation
- Structured logging with Serilog
- Dependency injection configuration

## Configuration

The worker service can be configured using `appsettings.json`. Report settings can be customized in the `ReportSettings` section.
