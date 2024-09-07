using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Required] [Range(1,200)] decimal Price,
    DateOnly ReleaseDate);