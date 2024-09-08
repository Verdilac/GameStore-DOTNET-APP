using GameStore.API.Dtos;
using GameStore.API.Entities;

namespace GameStore.API.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(
            genre.Id,
            genre.Name);
    }

    public static Genre ToEntity(this CreateGenreDto genreDto)
    {
        return new Genre()
        {
            Name = genreDto.Name
        };
    }
    
 
}