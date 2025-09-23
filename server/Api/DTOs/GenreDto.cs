using DataAccess;

namespace Api.DTOs;

public class GenreDto
{
    public GenreDto(Genre genre)
    {
        Id = genre.Id;
        Name = genre.Name;
        Createdat = genre.Createdat;
        BookIds = genre.Books?.Select(b => b.Id).ToList() ?? new List<string>();
    } 
    
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual ICollection<string> BookIds { get; set; } = new List<string>();
}