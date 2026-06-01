// QUAN-20260531-154643
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using System.Text.Json; // For audit logging

namespace ONENET.Application.Subjects.Commands
{
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, Unit>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly IAuditService _auditService;
        private readonly ILogger<UpdateSubjectCommandHandler> _logger;

        public UpdateSubjectCommandHandler(
            ISubjectRepository subjectRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IAuditService auditService,
            ILogger<UpdateSubjectCommandHandler> logger)
        {
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating subject with Id: {Id}", request.Id);

            var subject = await _subjectRepository.GetByIdAsync(request.Id, cancellationToken);
            if (subject == null)
            {
                _logger.LogWarning("Subject update failed: Subject with Id '{Id}' not found.", request.Id);
                throw new NotFoundException(nameof(Subject), request.Id);
            }

            // Kiểm tra trùng lặp Mã môn học (nếu Code được phép sửa và thay đổi)
            // BRD: "Mã môn học là duy nhất" -> cần kiểm tra mã mới nếu khác mã cũ
            // Assuming Code is also part of update request and should be unique.
            if (subject.Code != request.Code && await _subjectRepository.ExistsWithCodeAsync(request.Code, request.Id, cancellationToken))
            {
                _logger.LogWarning("Subject update failed: Code '{Code}' already exists for another subject.", request.Code);
                throw new ValidationException("code", $"Mã môn học '{request.Code}' đã tồn tại.");
            }

            // Kiểm tra trùng lặp Tên môn học
            if (subject.Name != request.Name && await _subjectRepository.ExistsWithNameAsync(request.Name, request.Id, cancellationToken))
            {
                    _logger.LogWarning("Subject update failed: Name '{Name}' already exists for another subject.", request.Name);
                    throw new ValidationException("name", $"Tên môn học '{request.Name}' đã tồn tại.");
            }

            // Capture old values for audit
            var originalSubject = JsonSerializer.Serialize(new
            {
                subject.Code,
                subject.Name,
                subject.Description,
                subject.Credits,
                subject.IsActive
            });

            // Update properties
            // Note: The Subject entity doesn't have a public setter for Code, so I'm assuming Code is not changeable through this method.
            // If Code is meant to be changeable, the Subject entity's Create/Update methods need adjustment, or a dedicated "UpdateCode" method.
            // Following the BRD 'Mã môn học là duy nhất', it implies uniqueness and maybe immutability after creation, but 'update' API contract allows it.
            // For now, I'll allow Code update based on API contract.
            // To update Code, I need to create a new method in Subject entity to handle this.
            // Let's modify Subject.Update to allow Code as well, or a separate method.
            // Re-evaluating SRS/BRD for update request: "code": "string (optional, max 20, unique)". It means code *can* be updated.
            // I need to update Subject.cs to allow Code update.
            // The Subject.Update method should take Code.

            // Subject entity Update method needs to accept code.
            // For now, I will modify the subject property directly. If complex logic is needed, add to Subject entity.
            // The Subject.Update method should also handle `Code` field.
            // Update the subject entity by calling the Update method.
            subject.Code = request.Code; // Direct assignment for now. Better to have a dedicated method in Entity.
            subject.Update(
                request.Name,
                request.Description,
                request.Credits,
                request.IsActive,
                _currentUser.UserName ?? "system_user"
            );

            _subjectRepository.Update(subject);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Ghi Audit Log
            await _auditService.LogAsync(
                "Update",
                subject,
                _currentUser.UserName ?? "system_user",
                new { OldValues = originalSubject, NewValues = JsonSerializer.Serialize(new { subject.Code, subject.Name, subject.Description, subject.Credits, subject.IsActive }) },
                cancellationToken
            );

            _logger.LogInformation("Subject with Id: {Id} updated successfully.", request.Id);
            return Unit.Value;
        }
    }
}