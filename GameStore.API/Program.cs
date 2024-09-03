using GameStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


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

app.Run();
