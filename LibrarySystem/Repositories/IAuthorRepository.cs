using LibrarySystem.Entities;

namespace LibrarySystem.Repositories
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllWithBooksAsync();
        Task<Author?> GetByIdWithBooksAsync(int id);
    }
}
