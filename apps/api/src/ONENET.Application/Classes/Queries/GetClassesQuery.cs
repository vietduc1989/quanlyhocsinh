// QUAN-20260530-2302
using MediatR;
using ONENET.Application.Classes.DTOs;
using ONENET.Application.Common.Models;

namespace ONENET.Application.Classes.Queries
{
    public record GetClassesQuery : IRequest<PaginatedList<ClassDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? Search { get; init; }
        public string? SchoolYear { get; init; }
        public string? HomeroomTeacherName { get; init; }
    }
}