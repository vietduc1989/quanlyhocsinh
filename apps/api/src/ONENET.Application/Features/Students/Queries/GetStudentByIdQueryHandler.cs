// QUAN-20260530-2301
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Features.Students.Dtos;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.Students.Queries
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentDetailDto> Handle(GetStudentByIdQuery request, CancellationToken ct)
        {
            var student = await _studentRepository.GetAll()
                                                  .AsNoTracking() // For read-only query
                                                  .Include(s => s.Lop)
                                                  .Include(s => s.TrangThaiHocSinh)
                                                  .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (student == null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }

            return _mapper.Map<StudentDetailDto>(student);
        }
    }
}