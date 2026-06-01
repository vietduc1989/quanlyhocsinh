using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.Students.Dtos;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.Students.Commands
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, SuccessDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<DeleteStudentCommandHandler> _logger;

        public DeleteStudentCommandHandler(
            IStudentRepository studentRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ILogger<DeleteStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<SuccessDto> Handle(DeleteStudentCommand request, CancellationToken ct)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, ct);
            if (student == null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }

            // QUAN-20260530-2301-FR06: Ngăn chặn xóa nếu có dữ liệu liên quan
            if (await _studentRepository.HasRelatedDataAsync(request.Id, ct))
            {
                throw new ConflictException(
                    "Không thể xóa học sinh vì có dữ liệu liên quan. Vui lòng xử lý dữ liệu liên quan trước.");
            }

            student.IsDeleted = true; // Soft delete
            student.UpdatedBy = _currentUser.UserId;
            student.UpdatedAt = DateTime.UtcNow;

            _studentRepository.Update(student); // Mark for update (soft delete)
            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("Student soft deleted: {StudentId} by {UserId}", student.Id, _currentUser.UserId);

            return new SuccessDto(true);
        }
    }
}
