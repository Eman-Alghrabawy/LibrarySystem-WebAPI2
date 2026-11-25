namespace LibrarySystem.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }

        // Many-to-One: Book -> Author
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        // Many-to-Many: Book <-> Borrower (through Loan)
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
