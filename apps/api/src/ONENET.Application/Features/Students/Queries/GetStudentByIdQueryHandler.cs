using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Features.Students.Dtos;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using ONENET.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.Students.Queries
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentDetailDto> Handle(GetStudentByIdQuery request, CancellationToken ct)
        {
            var student = await _context.Students
                                                  .AsNoTracking() // For read-only query
                                                  .Include(s => s.Lop)
                                                  .Include(s => s.TrangThai)
                                                  .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (student == null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }

            return _mapper.Map<StudentDetailDto>(student);
        }
    }
}
