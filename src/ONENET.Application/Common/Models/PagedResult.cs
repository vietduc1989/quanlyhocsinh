/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Application (Common Models)
 */

using System;
using System.Collections.Generic;

namespace ONENET.Application.Common.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Array.Empty<T>();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

        public PagedResult(IEnumerable<T> items, int currentPage, int pageSize, int totalRecords)
        {
            Items = items;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }
    }
}