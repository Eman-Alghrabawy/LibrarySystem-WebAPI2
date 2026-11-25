using LibrarySystem.Entities;
using LibrarySystem.Results;

namespace LibrarySystem.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllWithAuthorAsync();

        Task<PaginatedList<Book>> GetAllAsync(int pageNumber, int pageSize, string? searchTerm);

        Task<Book?> GetByIdWithAuthorAsync(int id);
    }
}
