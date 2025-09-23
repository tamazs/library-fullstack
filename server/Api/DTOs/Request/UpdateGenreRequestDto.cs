using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class UpdateGenreRequestDto
{
    [MinLength(1)] [Required]
    public string GenreId { get; set; } = null!;
    
    [MinLength(3)] [Required]
    public string Name { get; set; } = null!;
}