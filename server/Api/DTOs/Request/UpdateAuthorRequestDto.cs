using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class UpdateAuthorRequestDto
{
    [MinLength(1)] [Required]
    public string AuthorId {get; set;} = null!;
    
    [MinLength(3)]  [Required]
    public string Name {get; set;} = null!;
    
    [Required]
    public List<string> BookIds { get; set; } = new();
}