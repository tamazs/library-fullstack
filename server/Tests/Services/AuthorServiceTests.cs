using Api.DTOs.Request;
using Api.Services;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class AuthorServiceTests : ServiceTestsBase
{
    [Fact]
    public async Task GetAllAuthors_ReturnsListOfAuthorDtos()
    {
        // Arrange
        Context.Authors.AddRange(
            new Author { Id = "1", Name = "Author 1" },
            new Author { Id = "2", Name = "Author 2" }
        );
        await Context.SaveChangesAsync();
        var service = new AuthorService(Context);

        // Act
        var result = await service.GetAllAuthors();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateAuthor_CreatesAndReturnsAuthorDto()
    {
        // Arrange
        var service = new AuthorService(Context);
        var createAuthorDto = new CreateAuthorRequestDto { Name = "New Author" };

        // Act
        var result = await service.CreateAuthor(createAuthorDto);

        // Assert
        Assert.Equal("New Author", result.Name);
        Assert.Single(Context.Authors.ToList());
    }

    [Fact]
    public async Task UpdateAuthor_UpdatesAndReturnsAuthorDto()
    {
        // Arrange
        var author = new Author { Id = "1", Name = "Author 1" };
        Context.Authors.Add(author);
        await Context.SaveChangesAsync();
        var service = new AuthorService(Context);
        var updateAuthorDto = new UpdateAuthorRequestDto { AuthorId = "1", Name = "Updated Author", BookIds = new List<string>() };

        // Act
        var result = await service.UpdateAuthor(updateAuthorDto);

        // Assert
        Assert.Equal("Updated Author", result.Name);
        var authorInDb = await Context.Authors.FirstAsync();
        Assert.Equal("Updated Author", authorInDb.Name);
    }

    [Fact]
    public async Task DeleteAuthor_DeletesAndReturnsAuthorDto()
    {
        // Arrange
        var author = new Author { Id = "1", Name = "Author 1" };
        Context.Authors.Add(author);
        await Context.SaveChangesAsync();
        var service = new AuthorService(Context);

        // Act
        var result = await service.DeleteAuthor("1");

        // Assert
        Assert.Equal("1", result.Id);
        Assert.Empty(Context.Authors.ToList());
    }
}