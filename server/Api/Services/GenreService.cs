using System.ComponentModel.DataAnnotations;
using Api.DTOs;
using Api.DTOs.Request;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class GenreService (LibraryDbContext  dbContext)
{
    public async Task<List<GenreDto>> GetAllGenres()
    {
        return dbContext.Genres
            .Include(g => g.Books)
            .Select(g => new GenreDto(g)).ToList();
    }

    public async Task<GenreDto> CreateGenre(CreateGenreRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);

        var genre = new Genre
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Createdat = DateTime.UtcNow
        };
        
        await dbContext.Genres.AddAsync(genre);
        await dbContext.SaveChangesAsync();
        return new GenreDto(genre);
    }

    public async Task<GenreDto> UpdateGenre(UpdateGenreRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);

        var genre = await dbContext.Genres.FirstAsync(g => g.Id == dto.GenreId);
        genre.Name = dto.Name;
        
        await dbContext.SaveChangesAsync();
        return new GenreDto(genre);
    }

    public async Task<GenreDto> DeleteGenre(string id)
    {
        var genre = await dbContext.Genres.FirstAsync(g => g.Id == id);
        dbContext.Genres.Remove(genre);
        await dbContext.SaveChangesAsync();
        return new GenreDto(genre);
    }
}