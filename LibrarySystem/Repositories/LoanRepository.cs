using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Entities;

namespace LibrarySystem.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LoanRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<IEnumerable<Loan>> GetAllAsync()
            => await _dbContext.Loans
                .Include(l => l.Book)
                .Include(l => l.Borrower)
                .ToListAsync();

        public async Task<Loan?> GetByIdAsync(int bookId, int borrowerId)
            => await _dbContext.Loans
                .Include(l => l.Book)
                .Include(l => l.Borrower)
                .FirstOrDefaultAsync(l => l.BookId == bookId && l.BorrowerId == borrowerId);

        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _dbContext.Loans.AddAsync(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<bool> UpdateAsync(Loan loan)
        {
            var entity = await GetByIdAsync(loan.BookId, loan.BorrowerId);

            if (entity is null)
                return false;

            entity.LoanDate = loan.LoanDate;
            entity.ReturnDate = loan.ReturnDate;

            _dbContext.Loans.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int bookId, int borrowerId)
        {
            var entity = await GetByIdAsync(bookId, borrowerId);

            if (entity is null)
                return false;

            _dbContext.Loans.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
