namespace LibrarySystem.Results
{
    public class PaginatedList<T>
    {
        // Attributes
        private const int MaxPageSize = 20;
        private int _pageSize = 10;

        // Properties
        public int PageNumber { get; set; } = 1;
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public IEnumerable<T>? Data { get; set; }
    }
}
