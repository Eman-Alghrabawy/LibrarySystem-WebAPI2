using System.Text.Json;
using LibrarySystem.Entities;

namespace LibrarySystem.Data
{
    public static class StoreContextSeeding
    {
        // FIX (from the example): the seed files are located from the project's
        // Content Root, not from a hard-coded absolute path on one machine,
        // so seeding works on any computer.
        public async static Task SeedAsync(ApplicationDbContext dbContext, string contentRootPath)
        {
            var seedingFolder = Path.Combine(contentRootPath, "Data", "Seeding");

            if (!dbContext.Authors.Any())
            {
                var authorsData = await File.ReadAllTextAsync(Path.Combine(seedingFolder, "Authors.json"));
                var authors = JsonSerializer.Deserialize<List<Author>>(authorsData);

                if (authors is not null)
                {
                    await dbContext.Authors.AddRangeAsync(authors);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Borrowers.Any())
            {
                var borrowersData = await File.ReadAllTextAsync(Path.Combine(seedingFolder, "Borrowers.json"));
                var borrowers = JsonSerializer.Deserialize<List<Borrower>>(borrowersData);

                if (borrowers is not null)
                {
                    await dbContext.Borrowers.AddRangeAsync(borrowers);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Books.Any())
            {
                var booksData = await File.ReadAllTextAsync(Path.Combine(seedingFolder, "Books.json"));
                var books = JsonSerializer.Deserialize<List<Book>>(booksData);

                if (books is not null)
                {
                    await dbContext.Books.AddRangeAsync(books);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
