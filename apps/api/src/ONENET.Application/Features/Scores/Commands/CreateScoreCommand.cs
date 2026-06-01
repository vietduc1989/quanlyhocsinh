// QUAN-20260531-154643
using MediatR;
using FluentValidation;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using ONENET.Application.Common.Exceptions;
using ONENET.Domain.ValueObjects;
using Serilog; // For structured logging

namespace ONENET.Application.Features.Scores.Commands
{
    public record CreateScoreCommand(
        Guid StudentId,
        Guid SubjectId,
        Guid SemesterId,
        decimal Value
    ) : IRequest<Guid>; // Returns the ID of the newly created score

    public class CreateScoreCommandHandler : IRequestHandler<CreateScoreCommand, Guid>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IUnitOfWork _unitOfWork; // Assuming IUnitOfWork exists in Common/Interfaces
        private readonly ICurrentUser _currentUser;
        private readonly ILogger _logger;
        private readonly IScorePermissionService _permissionService;
        private readonly IAuditLogService _auditLogService;

        public CreateScoreCommandHandler(
            IScoreRepository scoreRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ILogger logger,
            IScorePermissionService permissionService,
            IAuditLogService auditLogService)
        {
            _scoreRepository = scoreRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _logger = logger;
            _permissionService = permissionService;
            _auditLogService = auditLogService;
        }

        public async Task<Guid> Handle(CreateScoreCommand request, CancellationToken cancellationToken)
        {
            // BR04: Check permission for the current user
            if (!_currentUser.IsAuthenticated)
            {
                _logger.Warning("Unauthorized attempt to create score. User not authenticated.");
                throw new ForbiddenException("You must be authenticated to create a score.");
            }

            var currentUserName = _currentUser.UserName ?? "Unknown";

            var hasPermission = await _permissionService.CanManageScoreAsync(
                request.StudentId,
                request.SubjectId,
                request.SemesterId,
                _currentUser.UserId,
                _currentUser.IsInRole("Admin"),
                cancellationToken);

            if (!hasPermission)
            {
                _logger.Warning("User {UserName} (ID: {UserId}) attempted to create score for StudentId {StudentId}, SubjectId {SubjectId}, SemesterId {SemesterId} without permission. Action Forbidden.",
                                currentUserName, _currentUser.UserId, request.StudentId, request.SubjectId, request.SemesterId);
                throw new ForbiddenException("You do not have permission to create scores for this student, subject, or semester.");
            }

            // BR02: Check for uniqueness before creating
            var exists = await _scoreRepository.ExistsByUniqueKeysAsync(
                request.StudentId,
                request.SubjectId,
                request.SemesterId,
                cancellationToken);

            if (exists)
            {
                _logger.Warning("Attempted to create duplicate score for StudentId {StudentId}, SubjectId {SubjectId}, SemesterId {SemesterId} by user {UserName}. Action Conflict.",
                                request.StudentId, request.SubjectId, request.SemesterId, currentUserName);
                throw new ConflictException("A score for this student, subject, and semester already exists.");
            }

            // BR01 & BR03: ScoreValue handles range and decimal places. Required fields are handled by validator.
            var scoreValue = ScoreValue.FromDecimal(request.Value);

            var score = Score.Create(
                request.StudentId,
                request.SubjectId,
                request.SemesterId,
                scoreValue,
                currentUserName); // Use current user for CreatedBy

            await _scoreRepository.AddAsync(score, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Audit Trail
            await _auditLogService.LogAuditAsync(
                score.Id,
                nameof(Score),
                "Create",
                null, // No old values for creation
                new { score.StudentId, score.SubjectId, score.SemesterId, score.Value, score.CreatedBy },
                currentUserName,
                cancellationToken);

            _logger.Information("Score created successfully: {ScoreId} by {UserName}.", score.Id, currentUserName);

            return score.Id;
        }
    }
}