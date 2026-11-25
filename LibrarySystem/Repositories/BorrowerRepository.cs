using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Entities;

namespace LibrarySystem.Repositories
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BorrowerRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<IEnumerable<Borrower>> GetAllWithLoansAsync()
            => await _dbContext.Borrowers
                .Include(b => b.Loans)
                .ThenInclude(l => l.Book)
                .ToListAsync();

        public async Task<IEnumerable<Borrower>> GetAllAsync()
            => await GetAllWithLoansAsync();

        public async Task<Borrower?> GetByIdWithLoansAsync(int id)
            => await _dbContext.Borrowers
                .Include(b => b.Loans)
                .ThenInclude(l => l.Book)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Borrower> GetByIdAsync(int id)
            => await _dbContext.Borrowers.FindAsync(id);

        public async Task<Borrower> CreateAsync(Borrower borrower)
        {
            await _dbContext.Borrowers.AddAsync(borrower);
            await _dbContext.SaveChangesAsync();
            return borrower;
        }

        public async Task<bool> UpdateAsync(Borrower borrower)
        {
            var entity = await GetByIdAsync(borrower.Id);

            if (entity is null)
                return false;

            _dbContext.Borrowers.Update(borrower);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null)
                return false;

            _dbContext.Borrowers.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
