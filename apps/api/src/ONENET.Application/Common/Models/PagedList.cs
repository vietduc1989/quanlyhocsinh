using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONENET.Application.Common.Models
{
    public class PagedList<T>
    {
        public IReadOnlyList<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;

        public PagedList(IReadOnlyList<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count(); // Get total count before pagination
            var items = await Task.Run(() => source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}