namespace LibrarySystem.Dtos.BookDtos
{
    public class GetBookResponse
    {
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
