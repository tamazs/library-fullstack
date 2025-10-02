using Api.Controllers;
using Api.DTOs;
using Api.DTOs.Request;
using Api.Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controllers;

public class BookControllerTests
{
    private readonly Mock<IBookService> _bookServiceMock;
    private readonly BookController _bookController;

    public BookControllerTests()
    {
        _bookServiceMock = new Mock<IBookService>();
        _bookController = new BookController(_bookServiceMock.Object);
    }

    [Fact]
    public async Task GetAllBooks_ReturnsOkObjectResult_WithAListOfBookDtos()
    {
        // Arrange
        var books = new List<BookDto>
        {
            new(new() { Id = "1", Title = "Book 1" }),
            new(new() { Id = "2", Title = "Book 2" })
        };
        _bookServiceMock.Setup(service => service.GetAllBooks()).ReturnsAsync(books);

        // Act
        var result = await _bookController.GetAllBooks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<BookDto>>(okResult.Value);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task CreateBook_ReturnsOkObjectResult_WithABookDto()
    {
        // Arrange
        var createBookDto = new CreateBookRequestDto() { Title = "New Book" };
        var book = new BookDto(new() { Id = "1", Title = "New Book" });
        _bookServiceMock.Setup(service => service.CreateBook(createBookDto)).ReturnsAsync(book);

        // Act
        var result = await _bookController.CreateBook(createBookDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<BookDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }

    [Fact]
    public async Task UpdateBook_ReturnsOkObjectResult_WithABookDto()
    {
        // Arrange
        var updateBookDto = new UpdateBookRequestDto() { BookId = "1", Title = "Updated Book" };
        var book = new BookDto(new() { Id = "1", Title = "Updated Book" });
        _bookServiceMock.Setup(service => service.UpdateBook(updateBookDto)).ReturnsAsync(book);

        // Act
        var result = await _bookController.UpdateBook(updateBookDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<BookDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }

    [Fact]
    public async Task DeleteBook_ReturnsOkObjectResult_WithABookDto()
    {
        // Arrange
        var book = new BookDto(new() { Id = "1", Title = "Book 1" });
        _bookServiceMock.Setup(service => service.DeleteBook("1")).ReturnsAsync(book);

        // Act
        var result = await _bookController.DeleteBook("1");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<BookDto>(okResult.Value);
        Assert.Equal("1", model.Id);
    }
}
