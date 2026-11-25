using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Entities;

namespace LibrarySystem.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<IEnumerable<Author>> GetAllWithBooksAsync()
            => await _dbContext.Authors.Include(a => a.Books).ToListAsync();

        public async Task<IEnumerable<Author>> GetAllAsync()
            => await GetAllWithBooksAsync();

        public async Task<Author?> GetByIdWithBooksAsync(int id)
            => await _dbContext.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Author> GetByIdAsync(int id)
            => await _dbContext.Authors.FindAsync(id);

        public async Task<Author> CreateAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();
            return author;
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            var entity = await GetByIdAsync(author.Id);

            if (entity is null)
                return false;

            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null)
                return false;

            _dbContext.Authors.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
