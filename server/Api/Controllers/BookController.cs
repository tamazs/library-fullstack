using Api.DTOs;
using Api.DTOs.Request;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class BookController  : ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet(nameof(GetAllBooks))]
    public async Task<ActionResult<List<BookDto>>> GetAllBooks()
    {
        try
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost(nameof(CreateBook))]
    public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookRequestDto dto)
    {
        try
        {
            var book = await _bookService.CreateBook(dto);
            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(nameof(UpdateBook))]
    public async Task<ActionResult<BookDto>> UpdateBook([FromBody] UpdateBookRequestDto dto)
    {
        try
        {
            var book = await _bookService.UpdateBook(dto);
            return Ok(book);
        } catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(nameof(DeleteBook))]
    public async Task<ActionResult<BookDto>> DeleteBook(string id)
    {
        try
        {
            var book = await _bookService.DeleteBook(id);
            return Ok(book);
        } catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }
}