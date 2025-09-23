using System.ComponentModel.DataAnnotations;
using Api.DTOs;
using Api.DTOs.Request;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class BookService (LibraryDbContext  dbContext)
{
    public async Task<List<BookDto>> GetAllBooks()
    {
        return await dbContext.Books
            .Include(b => b.Genre)
            .Include(b => b.Authors)
            .Select(b => new BookDto(b))
            .ToListAsync();
    }

    public async Task<BookDto> CreateBook(CreateBookRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);

        var book = new Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Pages = dto.Pages,
            Createdat = DateTime.UtcNow
        };
        
        await dbContext.Books.AddAsync(book);
        await dbContext.SaveChangesAsync();
        return new BookDto(book);
    }

    public async Task<BookDto> UpdateBook(UpdateBookRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);

        var book = await dbContext.Books.FirstAsync(b => b.Id == dto.BookId);
        await dbContext.Entry(book).Collection(b => b.Authors).LoadAsync();
        
        book.Title = dto.Title;
        book.Pages = dto.Pages;
        book.Genre = dto.GenreId != null ? await dbContext.Genres.FirstAsync(g => g.Id == dto.GenreId) : null;
        
        book.Authors.Clear();
        dto.AuthorIds.ForEach(id => book.Authors.Add(dbContext.Authors.First(a => a.Id == id)));
        
        await dbContext.SaveChangesAsync();
        return new BookDto(book);
    }

    public async Task<BookDto> DeleteBook(string id)
    {
        var book = await dbContext.Books.FirstAsync(b => b.Id == id);
        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync();
        return new BookDto(book);
    }
}