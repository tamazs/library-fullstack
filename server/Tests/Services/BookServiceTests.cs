using Api.DTOs.Request;
using Api.Services;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class BookServiceTests : ServiceTestsBase
{
    [Fact]
    public async Task GetAllBooks_ReturnsListOfBookDtos()
    {
        // Arrange
        Context.Books.AddRange(
            new Book { Id = "1", Title = "Book 1" },
            new Book { Id = "2", Title = "Book 2" }
        );
        await Context.SaveChangesAsync();
        var service = new BookService(Context);

        // Act
        var result = await service.GetAllBooks();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateBook_CreatesAndReturnsBookDto()
    {
        // Arrange
        var service = new BookService(Context);
        var createBookDto = new CreateBookRequestDto { Title = "New Book", Pages = 100 };

        // Act
        var result = await service.CreateBook(createBookDto);

        // Assert
        Assert.Equal("New Book", result.Title);
        Assert.Single(Context.Books.ToList());
    }

    [Fact]
    public async Task UpdateBook_UpdatesAndReturnsBookDto()
    {
        // Arrange
        var book = new Book { Id = "1", Title = "Book 1" };
        var genre = new Genre { Id = "1", Name = "Genre 1" };
        Context.Books.Add(book);
        Context.Genres.Add(genre);
        await Context.SaveChangesAsync();
        var service = new BookService(Context);
        var updateBookDto = new UpdateBookRequestDto { BookId = "1", Title = "Updated Book", Pages = 200, AuthorIds = new List<string>(), GenreId = "1"};

        // Act
        var result = await service.UpdateBook(updateBookDto);

        // Assert
        Assert.Equal("Updated Book", result.Title);
        var bookInDb = await Context.Books.FirstAsync();
        Assert.Equal("Updated Book", bookInDb.Title);
    }

    [Fact]
    public async Task DeleteBook_DeletesAndReturnsBookDto()
    {
        // Arrange
        var book = new Book { Id = "1", Title = "Book 1" };
        Context.Books.Add(book);
        await Context.SaveChangesAsync();
        var service = new BookService(Context);

        // Act
        var result = await service.DeleteBook("1");

        // Assert
        Assert.Equal("1", result.Id);
        Assert.Empty(Context.Books.ToList());
    }
}