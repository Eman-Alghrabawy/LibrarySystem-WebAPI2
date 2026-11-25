using LibrarySystem.Entities;

namespace LibrarySystem.Repositories
{
    public interface IBorrowerRepository : IGenericRepository<Borrower>
    {
        Task<IEnumerable<Borrower>> GetAllWithLoansAsync();
        Task<Borrower?> GetByIdWithLoansAsync(int id);
    }
}
