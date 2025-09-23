using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class CreateBookRequestDto
{
    [MinLength(1)] [Required]
    public string Title { get; set; } = null!;
    [Range(1, int.MaxValue)] [Required]
    public int Pages { get; set; }
}