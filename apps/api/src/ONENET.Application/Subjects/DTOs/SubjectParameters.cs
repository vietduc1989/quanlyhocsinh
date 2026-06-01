// QUAN-20260531-154643
namespace ONENET.Application.Subjects.DTOs
{
    public class SubjectParameters
    {
        private const int MAX_PAGE_SIZE = 100;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 20;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public string? SortBy { get; set; } = "name"; // Default sort by name
        public string SortOrder { get; set; } = "asc"; // Default sort order ascending
        public string? SearchQuery { get; set; }
        public bool IncludeInactive { get; set; } = false; // Mặc định không bao gồm môn học không hoạt động
    }
}