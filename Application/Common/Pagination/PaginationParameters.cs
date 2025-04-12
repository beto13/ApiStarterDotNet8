namespace Application.Common.Pagination
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 100;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? OrderBy { get; set; }
        public bool OrderDescending { get; set; } = false;

        public int ValidatedPageNumber => PageNumber <= 0 ? 1 : PageNumber;
        public int ValidatedPageSize => PageSize <= 0 ? 10 : (PageSize > MaxPageSize ? MaxPageSize : PageSize);
    }
}
