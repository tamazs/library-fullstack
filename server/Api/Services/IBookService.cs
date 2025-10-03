using Api.DTOs;
using Api.DTOs.Request;

namespace Api.Services;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooks();
    Task<BookDto> CreateBook(CreateBookRequestDto dto);
    Task<BookDto> UpdateBook(UpdateBookRequestDto dto);
    Task<BookDto> DeleteBook(string id);
}
