<!-- QUAN-20260530-2301 -->
using System;
using MediatR;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.Students.DTOs;

namespace ONENET.Application.Features.Students.Queries
{
    public record GetStudentsQuery(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchQuery = null,
        Guid? LopId = null,
        Guid? TrangThaiId = null,
        string? SortBy = "hoVaTen", // Default sort
        string? SortOrder = "asc" // Default order
    ) : IRequest<PagedListDto<StudentDto>>;
}