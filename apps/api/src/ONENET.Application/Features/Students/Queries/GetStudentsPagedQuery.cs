// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.Students.Dtos;
using System;

namespace ONENET.Application.Features.Students.Queries
{
    public record GetStudentsPagedQuery : PaginationParameters, IRequest<PagedList<StudentSummaryDto>>
    {
        public string? SearchQuery { get; init; }
        public Guid? LopId { get; init; }
        public Guid? TrangThaiId { get; init; }
    }
}