# GameStore.Api

A simple ASP.NET Core Web API for managing games and genres. This repository contains the `GameStore.Api` service, data layer, DTOs, and API endpoints used for CRUD operations and listing games by genre.

**Contents**
- `Data/` - EF Core context and data extensions
- `Endpoints/` - Minimal API endpoint definitions (`GamesEndpoints.cs`, `GenreEnpoints.cs`)
- `Dtos/` - Request/response DTOs
- `Models/` - EF entities (`Game`, `Genre`)
- `DataMigrations/` - EF Core migrations

## Requirements
- .NET 10 SDK (or matching SDK for `net10.0`) installed
- Optional: `dotnet-ef` tool for applying migrations

## Getting started (local)
1. Restore and build:

```
dotnet restore
dotnet build GameStore.Api
```

2. Apply EF migrations (if you want to create/update the database):

```
dotnet tool install --global dotnet-ef    # if not already installed
dotnet ef database update --project GameStore.Api
```

3. Run the API:

```
dotnet run --project GameStore.Api
```

By default the host/port are configured in `Properties/launchSettings.json` and `appsettings.json`.

## Configuration
- App configuration files: `appsettings.json`, `appsettings.Development.json`.
- Database connection string is configured in the project's configuration (check `appsettings.json` and `Data/GameStoreContext.cs`).

## API Endpoints (examples)
The API uses minimal endpoints defined in `Endpoints/`.

- List games
  - GET `/games`
  - Example: `curl http://localhost:5000/games`

- Get game details
  - GET `/games/{id}`
  - Example: `curl http://localhost:5000/games/1`

- Create a game
  - POST `/games`
  - Example:

```
curl -X POST http://localhost:5000/games \
  -H "Content-Type: application/json" \
  -d '{"title":"Example Game","genreId":1,"price":19.99}'
```

- Update a game
  - PUT `/games/{id}`

- Delete a game
  - DELETE `/games/{id}`

- List genres
  - GET `/genres`

Adjust the host/port to match what `dotnet run` prints or your `launchSettings.json`.

## Development notes
- Endpoints are in `Endpoints/GamesEndpoints.cs` and `Endpoints/GenreEnpoints.cs`.
- DTOs live in `Dtos/` and Models in `Models/`.
- Database migrations are in `DataMigrations/` — use `dotnet ef` to add or apply migrations.

## Testing & Building CI
This repository does not include a test project by default. To add tests, create an xUnit or NUnit test project and reference `GameStore.Api`.

## Contributing
- Fork, create a feature branch, and open a PR with a clear description.
- Follow .editorconfig / project conventions where present.

## License
No license specified. Add a `LICENSE` file if you intend to open-source this project.

---

If you'd like, I can add example responses for the endpoints, add OpenAPI/Swagger setup, or wire up a sample database connection string in `appsettings.Development.json`.