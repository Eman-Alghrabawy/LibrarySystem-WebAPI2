using LibrarySystem.Entities;

namespace LibrarySystem.Repositories
{
    // Loan has a composite key, so it cannot use IGenericRepository<T>
    // (which works with a single int Id). It gets its own repository.
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int bookId, int borrowerId);
        Task<Loan> CreateAsync(Loan loan);
        Task<bool> UpdateAsync(Loan loan);
        Task<bool> DeleteAsync(int bookId, int borrowerId);
    }
}
