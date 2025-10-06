using Api.DTOs;
using Api.DTOs.Request;
using DataAccess;

namespace Api.Services;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooks();
    Task<List<Book>> GetBooksPag(int skip, int take); 
    Task<BookDto> CreateBook(CreateBookRequestDto dto);
    Task<BookDto> UpdateBook(UpdateBookRequestDto dto);
    Task<BookDto> DeleteBook(string id);
}
