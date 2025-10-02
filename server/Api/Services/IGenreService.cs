using Api.DTOs;
using Api.DTOs.Request;

namespace Api.Services;

public interface IGenreService
{
    Task<List<GenreDto>> GetAllGenres();
    Task<GenreDto> CreateGenre(CreateGenreRequestDto dto);
    Task<GenreDto> UpdateGenre(UpdateGenreRequestDto dto);
    Task<GenreDto> DeleteGenre(string id);
}
