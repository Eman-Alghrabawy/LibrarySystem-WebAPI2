using LibrarySystem.Dtos.BookDtos;
using LibrarySystem.Results;

namespace LibrarySystem.Services
{
    public interface IBookService
    {
        Task<GenericResult<List<GetBookResponse>>> GetAllAsync();
        Task<GenericResult<PaginatedList<GetBookResponse>>> GetAllAsync(int pageNumber, int pageSize, string? searchTerm);
        Task<GenericResult<GetBookResponse>> GetByIdAsync(int id);
        Task<GenericResult<GetBookResponse>> CreateAsync(CreateBookRequest request);
        Task<bool> UpdateAsync(UpdateBookRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
