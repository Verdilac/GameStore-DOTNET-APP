using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.EndPoints;

public static class GenresEndpoints
{
    const string GetGenreEndpointName = "GetGenre";


    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres").WithParameterValidation();
        
        //-----------GENRES-----------
        //Get /genres

        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Genres
                .Select(genre =>genre.ToDto()).AsNoTracking().ToListAsync());
        
        //Get/Genres{id}
        group.MapGet("/{id}", async (int id,GameStoreContext dbContext) =>
        {
            Genre? genre = await  dbContext.Genres.FindAsync(id);
            return genre is null ? 
                Results.NotFound() : Results.Ok(genre.ToDto());
        }).WithName(GetGenreEndpointName); 
        
        group.MapPost("/", async (CreateGenreDto newGenre,GameStoreContext dbContext) =>
        {
            Genre genre = newGenre.ToEntity();
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetGenreEndpointName, new { id = genre.Id }, genre.ToDto());
        });
        
        return group;
    }
}