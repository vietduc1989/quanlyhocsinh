<!-- QUAN-20260530-2301 -->
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.Students.Commands;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteStudentCommandHandler> _logger;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork, ILogger<DeleteStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (student == null)
            {
                _logger.LogWarning("Failed to delete student: Student with ID '{StudentId}' not found.", request.Id);
                throw new NotFoundException("Student", request.Id);
            }

            // QUAN-20260530-2301-FR06: Ngăn chặn xóa học sinh nếu có dữ liệu liên quan
            // This is a placeholder for checking actual related data like grades, attendance, etc.
            // For now, we simulate a check. In a real scenario, this would query related tables.
            if (await _studentRepository.HasRelatedDataAsync(request.Id, cancellationToken))
            {
                _logger.LogWarning("Failed to delete student: Student {StudentId} has related data.", request.Id);
                throw new ConflictException("Không thể xóa học sinh vì có dữ liệu liên quan. Vui lòng xử lý dữ liệu liên quan trước.", "STUDENT_HAS_RELATED_DATA");
            }

            _studentRepository.Delete(student); // Soft delete handled by global query filter in EF Core
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Student deleted successfully (soft delete): {StudentId}", request.Id);
            return Unit.Value;
        }
    }
}