namespace Application.Common.Pagination
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Ordenamiento opcional
        public string? OrderBy { get; set; }
        public bool OrderDescending { get; set; } = false;

        // Evita pageSize excesivo
        private const int MaxPageSize = 100;

        public int ValidatedPageSize => (PageSize > MaxPageSize) ? MaxPageSize : PageSize;
    }
}
