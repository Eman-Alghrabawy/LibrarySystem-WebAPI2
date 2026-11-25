using LibrarySystem.Dtos.AuthorDtos;
using LibrarySystem.Results;

namespace LibrarySystem.Services
{
    public interface IAuthorService
    {
        Task<GenericResult<List<GetAuthorResponse>>> GetAllAsync();
        Task<GenericResult<GetAuthorResponse>> GetByIdAsync(int id);
        Task<GenericResult<GetAuthorResponse>> CreateAsync(CreateAuthorRequest request);
        Task<bool> UpdateAsync(UpdateAuthorRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
