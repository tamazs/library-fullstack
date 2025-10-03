using Api.DTOs;
using Api.DTOs.Request;

namespace Api.Services;

public interface IAuthorService
{
    Task<List<AuthorDto>> GetAllAuthors();
    Task<AuthorDto> CreateAuthor(CreateAuthorRequestDto dto);
    Task<AuthorDto> UpdateAuthor(UpdateAuthorRequestDto dto);
    Task<AuthorDto> DeleteAuthor(string id);
}