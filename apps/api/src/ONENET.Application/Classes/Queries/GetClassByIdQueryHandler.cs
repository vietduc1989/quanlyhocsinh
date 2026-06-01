// QUAN-20260530-2302
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Classes.DTOs;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using System.Linq;

namespace ONENET.Application.Classes.Queries
{
    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, ClassDto>
    {
        private readonly IApplicationDbContext _context;

        public GetClassByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClassDto> Handle(GetClassByIdQuery request, CancellationToken ct)
        {
            var classEntity = await _context.Classes
                .AsNoTracking()
                .Include(c => c.HomeroomTeacher)
                .Include(c => c.Students)
                .Where(c => c.Id == request.Id)
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    ClassCode = c.ClassCode,
                    ClassName = c.ClassName,
                    SchoolYear = c.SchoolYear,
                    CurrentStudentCount = c.Students.Count(s => !s.IsDeleted),
                    HomeroomTeacherId = c.HomeroomTeacherId,
                    HomeroomTeacherName = c.HomeroomTeacher != null ? c.HomeroomTeacher.FullName : null
                })
                .FirstOrDefaultAsync(ct);

            if (classEntity == null)
            {
                throw new NotFoundException($"Lớp học với ID '{request.Id}' không tìm thấy.");
            }

            return classEntity;
        }
    }
}