using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Entities;

namespace GameStore.API.EndPoints;

public static class GamesEndpoints
{
   
    
    const string GetGameEndpointName = "GetGame";
    
    private static readonly List<GameDto> games =
    [
        new(1,
            "Horizon Zero Dawn",
            "OpenWorld RPG",
            19.99M,
            new DateOnly(2017, 2, 2)),
        new(2,
            "CyberPunk",
            "OpenWorld RPG",
            59.99M,
            new DateOnly(2020, 1, 19)),
        new(
            3,
            "Elden Ring",
            "OpenWorld RPG",
            59.99M,
            new DateOnly(2021, 3, 7)),
        new(4,
            "Titan Fall 2",
            "Action",
            19.99M,
            new DateOnly(2016, 2, 2))
    ];



    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app) {

        var group = app.MapGroup("games").WithParameterValidation();
        
//  GET /games
        group.MapGet("/", ()=>games);

        group.MapGet("/{id}", (int id) =>
        { 
            GameDto? game =  games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);
        
//POST

        group.MapPost("/", (CreateGameDto newGame,GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                Genre = dbContext.Genre.Find(newGame.GenreId), //execute an sql query here to retrieve the Id
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            dbContext.Game.Add(game);
            dbContext.SaveChanges();

            GameDto gameDto = new(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );
            
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
        });

//PUT
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index  = games.FindIndex(game => game.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate);
    
            return Results.NoContent();
        });

//DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        return group;
    }
    

}