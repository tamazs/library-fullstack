using DataAccess;

namespace Api.DTOs;

public class AuthorDto
{
    public AuthorDto(Author author)
    {
        Id = author.Id;
        Name = author.Name;
        Createdat = author.Createdat;
        Books = author.Books?.Select(b => new BookDto(b)).ToList();
    }
    
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual ICollection<BookDto> Books { get; set; } = new List<BookDto>();
}