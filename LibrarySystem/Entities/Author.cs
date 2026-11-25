namespace LibrarySystem.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        // One-to-Many: Author -> Book
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
