using DataAccess;

namespace Api.DTOs;

public class BookDto
{
    public BookDto(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Pages = book.Pages;
        Createdat = book.Createdat;
        if (book.Genre != null) GenreId = book.Genreid;
        AuthorIds = book.Authors?.Select(a => a.Id).ToList() ?? new List<string>();
    }
    
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int Pages { get; set; }

    public DateTime? Createdat { get; set; }

    public string? GenreId { get; set; }

    public virtual ICollection<string> AuthorIds { get; set; } = new List<string>();
}