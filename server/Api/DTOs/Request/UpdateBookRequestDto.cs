using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class UpdateBookRequestDto
{
    [MinLength(1)] [Required]
    public string BookId { get; set; } = null!;
    
    [MinLength(1)] [Required]
    public string Title { get; set; } = null!;
    
    [Range(1, int.MaxValue)] [Required]
    public int Pages { get; set; }
    
    [Required]
    public List<string> AuthorIds { get; set; } = new();
    
    [Required]
    public string GenreId { get; set; } = null!;
}