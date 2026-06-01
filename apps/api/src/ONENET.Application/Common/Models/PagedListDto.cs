using System.Collections.Generic;

namespace ONENET.Application.Common.Models
{
    public class PagedListDto<T>
    {
        public IReadOnlyList<T> Items { get; init; } = new List<T>();
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;

        public PagedListDto() { } // Required for deserialization

        public PagedListDto(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}