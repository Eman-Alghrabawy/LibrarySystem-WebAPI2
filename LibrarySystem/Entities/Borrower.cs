namespace LibrarySystem.Entities
{
    public class Borrower : BaseEntity
    {
        public string Name { get; set; }
        public DateTime MembershipDate { get; set; }

        // Many-to-Many: Borrower <-> Book (through Loan)
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
