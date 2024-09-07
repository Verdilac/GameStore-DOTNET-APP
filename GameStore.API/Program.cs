using GameStore.API.Data;
using GameStore.API.EndPoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("GameStore");
//builder.Services.AddSqlServer<GameStoreContext>(connString);

builder.Services.AddDbContext<GameStoreContext>(options =>
{
    options.UseSqlServer(connString);
});


var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb();
app.Run();
