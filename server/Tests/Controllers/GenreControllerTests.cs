using Api.Controllers;
using Api.DTOs;
using Api.DTOs.Request;
using Api.Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controllers;

public class GenreControllerTests
{
    private readonly Mock<IGenreService> _genreServiceMock;
    private readonly GenreController _genreController;

    public GenreControllerTests()
    {
        _genreServiceMock = new Mock<IGenreService>();
        _genreController = new GenreController(_genreServiceMock.Object);
    }

    [Fact]
    public async Task GetAllGenres_ReturnsOkObjectResult_WithAListOfGenreDtos()
    {
        // Arrange
        var genres = new List<GenreDto>
        {
            new(new() { Id = "1", Name = "Genre 1" }),
            new(new() { Id = "2", Name = "Genre 2" })
        };
        _genreServiceMock.Setup(service => service.GetAllGenres()).ReturnsAsync(genres);

        // Act
        var result = await _genreController.GetAllGenres();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<GenreDto>>(okResult.Value);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task CreateGenre_ReturnsOkObjectResult_WithAGenreDto()
    {
        // Arrange
        var createGenreDto = new CreateGenreRequestDto() { Name = "New Genre" };
        var genre = new GenreDto(new() { Id = "1", Name = "New Genre" });
        _genreServiceMock.Setup(service => service.CreateGenre(createGenreDto)).ReturnsAsync(genre);

        // Act
        var result = await _genreController.CreateGenre(createGenreDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<GenreDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }

    [Fact]
    public async Task UpdateGenre_ReturnsOkObjectResult_WithAGenreDto()
    {
        // Arrange
        var updateGenreDto = new UpdateGenreRequestDto() { GenreId = "1", Name = "Updated Genre" };
        var genre = new GenreDto(new() { Id = "1", Name = "Updated Genre" });
        _genreServiceMock.Setup(service => service.UpdateGenre(updateGenreDto)).ReturnsAsync(genre);

        // Act
        var result = await _genreController.UpdateGenre(updateGenreDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<GenreDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }

    [Fact]
    public async Task DeleteGenre_ReturnsOkObjectResult_WithAGenreDto()
    {
        // Arrange
        var genre = new GenreDto(new() { Id = "1", Name = "Genre 1" });
        _genreServiceMock.Setup(service => service.DeleteGenre("1")).ReturnsAsync(genre);

        // Act
        var result = await _genreController.DeleteGenre("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<GenreDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }
}
