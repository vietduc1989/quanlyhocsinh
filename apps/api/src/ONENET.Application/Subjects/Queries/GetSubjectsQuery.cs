// QUAN-20260531-154643
using MediatR;
using ONENET.Application.Common.Models;
using ONENET.Application.Subjects.DTOs;

namespace ONENET.Application.Subjects.Queries
{
    public record GetSubjectsQuery : IRequest<PaginatedList<SubjectDto>>
    {
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 20;
        public string? SortBy { get; init; } = "name";
        public string SortOrder { get; init; } = "asc";
        public string? SearchQuery { get; init; }
        public bool IncludeInactive { get; init; } = false;
    }
}