using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibrarySystem.Entities;

namespace LibrarySystem.Data.Configurations
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(20);

            // No two books share the same ISBN
            builder.HasIndex(b => b.ISBN)
                .IsUnique();

            // One-to-Many: Author -> Book
            builder.HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId);

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}
