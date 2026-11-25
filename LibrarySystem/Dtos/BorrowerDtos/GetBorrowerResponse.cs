namespace LibrarySystem.Dtos.BorrowerDtos
{
    public class GetBorrowerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime MembershipDate { get; set; }
        public List<string> BorrowedBookTitles { get; set; } = new();
    }
}
