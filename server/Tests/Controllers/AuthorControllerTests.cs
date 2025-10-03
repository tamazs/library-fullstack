using Api.Controllers;
using Api.DTOs;
using Api.DTOs.Request;
using Api.Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controllers;

public class AuthorControllerTests
{
    private readonly Mock<IAuthorService> _authorServiceMock;
    private readonly AuthorController _authorController;

    public AuthorControllerTests()
    {
        _authorServiceMock = new Mock<IAuthorService>();
        _authorController = new AuthorController(_authorServiceMock.Object);
    }

    [Fact]
    public async Task GetAllAuthors_ReturnsOkObjectResult_WithAListOfAuthorDtos()
    {
        // Arrange
        var authors = new List<AuthorDto>
        {
            new(new() { Id = "1", Name = "Author 1" }),
            new(new() { Id = "2", Name = "Author 2" })
        };
        _authorServiceMock.Setup(service => service.GetAllAuthors()).ReturnsAsync(authors);

        // Act
        var result = await _authorController.GetAllAuthors();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<AuthorDto>>(okResult.Value);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task CreateAuthor_ReturnsOkObjectResult_WithAnAuthorDto()
    {
        // Arrange
        var createAuthorDto = new CreateAuthorRequestDto() { Name = "New Author" };
        var author = new AuthorDto(new() { Id = "1", Name = "New Author" });
        _authorServiceMock.Setup(service => service.CreateAuthor(createAuthorDto)).ReturnsAsync(author);

        // Act
        var result = await _authorController.CreateAuthor(createAuthorDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<AuthorDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }

    [Fact]
    public async Task UpdateAuthor_ReturnsOkObjectResult_WithAnAuthorDto()
    {
        // Arrange
        var updateAuthorDto = new UpdateAuthorRequestDto() { AuthorId = "1", Name = "Updated Author" };
        var author = new AuthorDto(new() { Id = "1", Name = "Updated Author" });
        _authorServiceMock.Setup(service => service.UpdateAuthor(updateAuthorDto)).ReturnsAsync(author);

        // Act
        var result = await _authorController.UpdateAuthor(updateAuthorDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<AuthorDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }

    [Fact]
    public async Task DeleteAuthor_ReturnsOkObjectResult_WithAnAuthorDto()
    {
        // Arrange
        var author = new AuthorDto(new() { Id = "1", Name = "Author 1" });
        _authorServiceMock.Setup(service => service.DeleteAuthor("1")).ReturnsAsync(author);

        // Act
        var result = await _authorController.DeleteAuthor("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<AuthorDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }
}