using System.ComponentModel.DataAnnotations;
using Api.DTOs;
using Api.DTOs.Request;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AuthorService(LibraryDbContext dbContext) : IAuthorService
{
    public async Task<List<AuthorDto>> GetAllAuthors()
    {
        return await dbContext.Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.Genre)
            .Select(a => new AuthorDto(a))
            .ToListAsync();
    }

    public async Task<AuthorDto> CreateAuthor(CreateAuthorRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);

        var author = new Author
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Createdat = DateTime.UtcNow
        };

        await dbContext.Authors.AddAsync(author);
        await dbContext.SaveChangesAsync();
        return new AuthorDto(author);
    }

    public async Task<AuthorDto> UpdateAuthor(UpdateAuthorRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);

        var author = await dbContext.Authors.FirstAsync(a => a.Id == dto.AuthorId);
        await dbContext.Entry(author).Collection(a => a.Books).LoadAsync();
        author.Books.Clear();
        dto.BookIds.ForEach(id => author.Books.Add(dbContext.Books.First(b => b.Id == id)));
        author.Name = dto.Name;
        await dbContext.SaveChangesAsync();
        return new AuthorDto(author);
    }

    public async Task<AuthorDto> DeleteAuthor(string id)
    {
        var author = await dbContext.Authors.FirstAsync(a => a.Id == id);
        dbContext.Authors.Remove(author);
        await dbContext.SaveChangesAsync();
        return new AuthorDto(author);
    }
}