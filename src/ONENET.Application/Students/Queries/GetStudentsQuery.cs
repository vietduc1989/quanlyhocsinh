/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Application (Queries)
 */

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Application.Common.Models;
using ONENET.Domain.Entities;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Queries
{
    public record GetStudentsQuery(
        string? SearchQuery,
        int Page = 1,
        int PageSize = 20
    ) : IRequest<PagedResult<Student>>;

    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, PagedResult<Student>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<PagedResult<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await _studentRepository.GetPagedAsync(
                request.SearchQuery,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            return new PagedResult<Student>(items, request.Page, request.PageSize, totalCount);
        }
    }
}