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
    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, Unit>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly IAuditService _auditService;
        private readonly ILogger<DeleteSubjectCommandHandler> _logger;

        public DeleteSubjectCommandHandler(
            ISubjectRepository subjectRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IAuditService auditService,
            ILogger<DeleteSubjectCommandHandler> logger)
        {
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to soft delete subject with Id: {Id}", request.Id);

            var subject = await _subjectRepository.GetByIdAsync(request.Id, cancellationToken);
            if (subject == null)
            {
                _logger.LogWarning("Subject soft delete failed: Subject with Id '{Id}' not found.", request.Id);
                throw new NotFoundException(nameof(Subject), request.Id);
            }

            // Capture old values for audit
            var originalIsActive = subject.IsActive;

            subject.SoftDelete(_currentUser.UserName ?? "system_user");
            _subjectRepository.Update(subject); // Update will handle IsDeleted = true

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Ghi Audit Log
            await _auditService.LogAsync(
                "SoftDelete",
                subject,
                _currentUser.UserName ?? "system_user",
                JsonSerializer.Serialize(new { OldIsActive = originalIsActive, NewIsActive = subject.IsActive, subject.IsDeleted }),
                cancellationToken
            );

            _logger.LogInformation("Subject with Id: {Id} soft deleted successfully.", request.Id);
            return Unit.Value;
        }
    }
}