using Dtos.GameDto;
using Dtos.UpdateGameDto;
using GameStore.Api.Data;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameSummaryDto> games =
    [
        new (1, "Street Fighter", "Fighting", 19.99m, new DateOnly(1992,7,15)),
        new (2, "Astro Bot", "Platformer", 19.99m, new DateOnly(1992,7,15)),
        new (3, "Final Fantasy", "RPG", 19.99m, new DateOnly(1992,7,15)),
    ];

    public async static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        // GET /games 
        group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games
            .Include(game => game.Genre)
            .Select(game => new GameSummaryDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        ))
        .AsNoTracking()
        .ToListAsync()
        );

        // GET /games/:id
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            // var game = games.Find(game => game.Id == id);
            var game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate)
            );
        })
            .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            // GameDto game = new (
            //     games.Count+1,
            //     newGame.Name,
            //     newGame.Genre,
            //     newGame.Price,
            //     newGame.ReleaseDate 
            // );
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            await dbContext.Games.AddAsync(game);
            //games.Add(game);
            await dbContext.SaveChangesAsync();//translate pending changes into sql statement and save
            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
        });

        // PUT /games/id
        group.MapPut("/{id}", async (UpdateGameDto updatedGame, int id, GameStoreContext dbContext) =>
        {
            // var index = games.FindIndex(game => game.Id == id);
            var existingGame = await dbContext.Games.FindAsync(id);
            // var game = new GameSummaryDto
            // (
            //     id,
            //     updatedGame.Name,
            //     updatedGame.GenreId,
            //     updatedGame.Price,
            //     updatedGame.ReleaseDate
            // );
            if (existingGame is null) return Results.NotFound();

            existingGame.Name = updatedGame.Name;
            existingGame.GenreId = updatedGame.GenreId;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = existingGame.Id }, updatedGame);
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext
            .Games
            .Where(game => game.Id == id)
            .ExecuteDeleteAsync();
            //games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

    }
}