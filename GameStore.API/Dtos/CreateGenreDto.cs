using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record CreateGenreDto(
    [Required][StringLength(50)] string Name);