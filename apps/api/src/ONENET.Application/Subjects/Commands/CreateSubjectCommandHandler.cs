// QUAN-20260531-154643
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using ONENET.Application.Common.Exceptions;
using System.Text.Json; // For audit logging

namespace ONENET.Application.Subjects.Commands
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Guid>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly IAuditService _auditService;
        private readonly ILogger<CreateSubjectCommandHandler> _logger;

        public CreateSubjectCommandHandler(
            ISubjectRepository subjectRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IAuditService auditService,
            ILogger<CreateSubjectCommandHandler> logger)
        {
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating subject with Code: {Code}, Name: {Name}", request.Code, request.Name);

            // Kiểm tra trùng lặp Mã môn học
            if (await _subjectRepository.ExistsWithCodeAsync(request.Code, null, cancellationToken))
            {
                _logger.LogWarning("Subject creation failed: Code '{Code}' already exists.", request.Code);
                throw new ValidationException("code", $"Mã môn học '{request.Code}' đã tồn tại.");
            }

            // Kiểm tra trùng lặp Tên môn học
            if (await _subjectRepository.ExistsWithNameAsync(request.Name, null, cancellationToken))
            {
                _logger.LogWarning("Subject creation failed: Name '{Name}' already exists.", request.Name);
                throw new ValidationException("name", $"Tên môn học '{request.Name}' đã tồn tại.");
            }

            var subject = Subject.Create(
                request.Code,
                request.Name,
                request.Description,
                request.Credits,
                _currentUser.UserName ?? "system_user" // Lấy người tạo từ context
            );
            subject.IsActive = request.IsActive; // Set isActive based on request, default true

            await _subjectRepository.AddAsync(subject, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Ghi Audit Log
            await _auditService.LogAsync(
                "Create",
                subject,
                _currentUser.UserName ?? "system_user",
                JsonSerializer.Serialize(new { subject.Code, subject.Name, subject.Credits, subject.IsActive }),
                cancellationToken
            );

            _logger.LogInformation("Subject created successfully with Id: {SubjectId}", subject.Id);
            return subject.Id;
        }
    }
}