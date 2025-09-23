using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Request;

public class CreateAuthorRequestDto
{
    [MinLength(3)] [Required]
    public string Name { get; set; } = null!;
}