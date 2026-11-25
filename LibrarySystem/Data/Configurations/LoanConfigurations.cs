using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibrarySystem.Entities;

namespace LibrarySystem.Data.Configurations
{
    public class LoanConfigurations : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            // Composite Primary Key: the same borrower cannot borrow
            // the same book twice in the same loan record.
            builder.HasKey(l => new { l.BookId, l.BorrowerId });

            builder.Property(l => l.LoanDate)
                .IsRequired();

            // Many-to-Many: Book <-> Borrower through Loan
            builder.HasOne(l => l.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.BookId);

            builder.HasOne(l => l.Borrower)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.BorrowerId);
        }
    }
}
