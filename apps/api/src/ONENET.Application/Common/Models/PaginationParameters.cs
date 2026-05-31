// QUAN-20260530-2301
namespace ONENET.Application.Common.Models
{
    public abstract class PaginationParameters
    {
        private const int MAX_PAGE_SIZE = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } = "asc"; // "asc" or "desc"
    }
}