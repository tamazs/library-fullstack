using Api.DTOs.Request;
using Api.Services;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class GenreServiceTests : ServiceTestsBase
{
    [Fact]
    public async Task GetAllGenres_ReturnsListOfGenreDtos()
    {
        // Arrange
        Context.Genres.AddRange(
            new Genre { Id = "1", Name = "Genre 1" },
            new Genre { Id = "2", Name = "Genre 2" }
        );
        await Context.SaveChangesAsync();
        var service = new GenreService(Context);

        // Act
        var result = await service.GetAllGenres();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateGenre_CreatesAndReturnsGenreDto()
    {
        // Arrange
        var service = new GenreService(Context);
        var createGenreDto = new CreateGenreRequestDto { Name = "New Genre" };

        // Act
        var result = await service.CreateGenre(createGenreDto);

        // Assert
        Assert.Equal("New Genre", result.Name);
        Assert.Single(Context.Genres.ToList());
    }

    [Fact]
    public async Task UpdateGenre_UpdatesAndReturnsGenreDto()
    {
        // Arrange
        var genre = new Genre { Id = "1", Name = "Genre 1" };
        Context.Genres.Add(genre);
        await Context.SaveChangesAsync();
        var service = new GenreService(Context);
        var updateGenreDto = new UpdateGenreRequestDto { GenreId = "1", Name = "Updated Genre" };

        // Act
        var result = await service.UpdateGenre(updateGenreDto);

        // Assert
        Assert.Equal("Updated Genre", result.Name);
        var genreInDb = await Context.Genres.FirstAsync();
        Assert.Equal("Updated Genre", genreInDb.Name);
    }

    [Fact]
    public async Task DeleteGenre_DeletesAndReturnsGenreDto()
    {
        // Arrange
        var genre = new Genre { Id = "1", Name = "Genre 1" };
        Context.Genres.Add(genre);
        await Context.SaveChangesAsync();
        var service = new GenreService(Context);

        // Act
        var result = await service.DeleteGenre("1");

        // Assert
        Assert.Equal("1", result.Id);
        Assert.Empty(Context.Genres.ToList());
    }
}