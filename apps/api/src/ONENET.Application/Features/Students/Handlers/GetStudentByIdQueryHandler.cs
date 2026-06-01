<!-- QUAN-20260530-2301 -->
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Features.Students.DTOs;
using ONENET.Application.Features.Students.Queries;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetStudentByIdQueryHandler> _logger;

        public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper, ILogger<GetStudentByIdQueryHandler> logger)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StudentDetailDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            // Eager load Lop and TrangThai for detail view
            var student = await ((ONENET.Infrastructure.Persistence.Repositories.StudentRepository)_studentRepository).GetStudentDetailsAsync(request.Id, cancellationToken);

            if (student == null)
            {
                _logger.LogWarning("Get student by ID failed: Student with ID '{StudentId}' not found.", request.Id);
                throw new NotFoundException("Student", request.Id);
            }

            var studentDto = _mapper.Map<StudentDetailDto>(student);
            _logger.LogInformation("Retrieved student details for ID: {StudentId}", request.Id);
            return studentDto;
        }
    }
}