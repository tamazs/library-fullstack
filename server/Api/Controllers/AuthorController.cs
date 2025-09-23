using Api.DTOs;
using Api.DTOs.Request;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet(nameof(GetAllAuthors))]
    public async Task<ActionResult<List<AuthorDto>>> GetAllAuthors()
    {
        try
        {
            var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost(nameof(CreateAuthor))]
    public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] CreateAuthorRequestDto dto)
    {
        try
        {
            var author = await _authorService.CreateAuthor(dto);
            return Ok(author);
        }
        catch  (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(nameof(UpdateAuthor))]
    public async Task<ActionResult<AuthorDto>> UpdateAuthor([FromBody] UpdateAuthorRequestDto dto)
    {
        try
        {
            var author = await _authorService.UpdateAuthor(dto);
            return Ok(author);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(nameof(DeleteAuthor))]
    public async Task<ActionResult<AuthorDto>> DeleteAuthor(string authorId)
    {
        try
        {
            var author = await _authorService.DeleteAuthor(authorId);
            return Ok(author);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}