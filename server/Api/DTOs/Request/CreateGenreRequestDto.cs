using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class CreateGenreRequestDto
{
    [MinLength(3)] [Required]
    public string Name { get; set; } = null!;
}