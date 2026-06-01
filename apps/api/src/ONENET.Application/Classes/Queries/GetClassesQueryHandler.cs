// QUAN-20260530-2302
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Classes.DTOs;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Domain.Entities;
using System.Linq.Expressions;
using System.Linq;

namespace ONENET.Application.Classes.Queries
{
    public class GetClassesQueryHandler : IRequestHandler<GetClassesQuery, PaginatedList<ClassDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetClassesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<ClassDto>> Handle(GetClassesQuery request, CancellationToken ct)
        {
            var query = _context.Classes
                .AsNoTracking()
                .Include(c => c.HomeroomTeacher)
                .Include(c => c.Students)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var searchLower = request.Search.ToLower();
                query = query.Where(c =>
                    c.ClassCode.ToLower().Contains(searchLower) ||
                    c.ClassName.ToLower().Contains(searchLower) ||
                    c.SchoolYear.ToLower().Contains(searchLower) ||
                    (c.HomeroomTeacher != null && c.HomeroomTeacher.FullName.ToLower().Contains(searchLower)));
            }

            if (!string.IsNullOrWhiteSpace(request.SchoolYear))
            {
                query = query.Where(c => c.SchoolYear == request.SchoolYear);
            }

            if (!string.IsNullOrWhiteSpace(request.HomeroomTeacherName))
            {
                var teacherNameLower = request.HomeroomTeacherName.ToLower();
                query = query.Where(c =>
                    c.HomeroomTeacher != null && c.HomeroomTeacher.FullName.ToLower().Contains(teacherNameLower));
            }

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(c => c.SchoolYear)
                .ThenBy(c => c.ClassCode)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    ClassCode = c.ClassCode,
                    ClassName = c.ClassName,
                    SchoolYear = c.SchoolYear,
                    CurrentStudentCount = c.Students.Count(s => !s.IsDeleted), // Only count non-deleted students
                    HomeroomTeacherId = c.HomeroomTeacherId,
                    HomeroomTeacherName = c.HomeroomTeacher != null ? c.HomeroomTeacher.FullName : null
                })
                .ToListAsync(ct);

            return new PaginatedList<ClassDto>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }
}