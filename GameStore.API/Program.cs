using GameStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games =
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

//  GET /games
app.MapGet("games", ()=>games);

app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);


//POST

app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName,new {id = game.Id}, game);
});

//PUT
app.MapPut("Games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index  = games.FindIndex(game => game.Id == id);
    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate);
    
    return Results.NoContent();
});

app.Run();
