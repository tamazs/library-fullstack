using Api.DTOs;
using Api.DTOs.Request;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class GenreController  : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet(nameof(GetAllGenres))]
    public async Task<ActionResult<List<GenreDto>>> GetAllGenres()
    {
        try
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost(nameof(CreateGenre))]
    public async Task<ActionResult<GenreDto>> CreateGenre([FromBody] CreateGenreRequestDto dto)
    {
        try
        {
            var genre = await _genreService.CreateGenre(dto);
            return Ok(genre);
        } catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(nameof(UpdateGenre))]
    public async Task<ActionResult<GenreDto>> UpdateGenre([FromBody] UpdateGenreRequestDto dto)
    {
        try
        {
            var genre = await _genreService.UpdateGenre(dto);
            return Ok(genre);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(nameof(DeleteGenre))]
    public async Task<ActionResult<GenreDto>> DeleteGenre([FromQuery] string id)
    {
        try
        {
            var genre = await _genreService.DeleteGenre(id);
            return Ok(genre);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}