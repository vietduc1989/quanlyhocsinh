// QUAN-20260531-154643
using ONENET.Application.Common.Models; // Assuming PaginatedList is in Common.Models

namespace ONENET.Application.Features.Scores.DTOs
{
    public class ScoreDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentFullName { get; set; } = string.Empty;
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Guid SemesterId { get; set; }
        public string SemesterName { get; set; } = string.Empty;
        public string SchoolYear { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }

    public class ScoreListItemDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentFullName { get; set; } = string.Empty;
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Guid SemesterId { get; set; }
        public string SemesterName { get; set; } = string.Empty;
        public string SchoolYear { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }

    // Assuming a generic PaginatedList<T> exists.
    // If not, I'd define it in ONENET.Application.Common.Models.
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList() { }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            Items = items;
        }
    }
}