using GameStore.API.Data;
using GameStore.API.EndPoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("GameStore");
//builder.Services.AddSqlServer<GameStoreContext>(connString);


// Registering GameStoreContext with the DI container.
builder.Services.AddDbContext<GameStoreContext>(options =>
{
    options.UseSqlServer(connString);
});


var app = builder.Build();

// Registering the endpoints.
app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();
app.Run();
