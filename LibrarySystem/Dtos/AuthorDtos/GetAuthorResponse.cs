namespace LibrarySystem.Dtos.AuthorDtos
{
    public class GetAuthorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public List<string> BookTitles { get; set; } = new();
    }
}
