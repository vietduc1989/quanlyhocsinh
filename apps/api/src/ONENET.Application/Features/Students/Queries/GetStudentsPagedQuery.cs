using MediatR;
using ONENET.Application.Common.Models;
using ONENET.Application.Features.Students.Dtos;
using System;

namespace ONENET.Application.Features.Students.Queries
{
    public class GetStudentsPagedQuery : PaginationParameters, IRequest<PagedList<StudentSummaryDto>>
    {
        public string? SearchQuery { get; set; }
        public Guid? LopId { get; set; }
        public Guid? TrangThaiId { get; set; }
    }
}
