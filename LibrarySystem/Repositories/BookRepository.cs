using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Entities;
using LibrarySystem.Results;

namespace LibrarySystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<IEnumerable<Book>> GetAllWithAuthorAsync()
            => await _dbContext.Books.Include(b => b.Author).ToListAsync();

        public async Task<IEnumerable<Book>> GetAllAsync()
            => await GetAllWithAuthorAsync();

        public async Task<PaginatedList<Book>> GetAllAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            var query = _dbContext.Books.Include(b => b.Author).AsQueryable();

            if (searchTerm is not null)
                query = query.Where(b => b.Title.Contains(searchTerm));

            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<Book>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalRecords = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Data = books
            };
        }

        public async Task<Book?> GetByIdWithAuthorAsync(int id)
            => await _dbContext.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Book> GetByIdAsync(int id)
            => await _dbContext.Books.FindAsync(id);

        public async Task<Book> CreateAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var entity = await GetByIdAsync(book.Id);

            if (entity is null)
                return false;

            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null)
                return false;

            _dbContext.Books.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
