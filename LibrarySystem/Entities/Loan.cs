namespace LibrarySystem.Entities
{
    // Join Entity for the Many-to-Many relationship between Book and Borrower.
    // It does NOT inherit from BaseEntity: its Primary Key is the composite
    // key (BookId + BorrowerId), configured in LoanConfigurations.
    public class Loan
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }

        public DateTime LoanDate { get; set; }

        // Null while the book is still borrowed; set when it is returned.
        public DateTime? ReturnDate { get; set; }
    }
}
