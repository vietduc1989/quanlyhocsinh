// QUAN-20260531-154643
using MediatR;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Interfaces;
using ONENET.Application.Common.Exceptions;
using Serilog;

namespace ONENET.Application.Features.Scores.Commands
{
    public record DeleteScoreCommand(Guid Id) : IRequest; // No return value for delete

    public class DeleteScoreCommandHandler : IRequestHandler<DeleteScoreCommand>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger _logger;
        private readonly IScorePermissionService _permissionService;
        private readonly IAuditLogService _auditLogService;

        public DeleteScoreCommandHandler(
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

        public async Task Handle(DeleteScoreCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                _logger.Warning("Unauthorized attempt to delete score. User not authenticated.");
                throw new ForbiddenException("You must be authenticated to delete a score.");
            }

            var score = await _scoreRepository.GetByIdAsync(request.Id, cancellationToken);

            if (score == null || score.IsDeleted)
            {
                _logger.Warning("Score not found for delete: {ScoreId} by user {UserName}.", request.Id, _currentUser.UserName);
                throw new NotFoundException($"Score with ID {request.Id} not found.");
            }

            var currentUserName = _currentUser.UserName ?? "Unknown";

            // BR04: Check permission for the current user
            var hasPermission = await _permissionService.CanManageScoreAsync(
                score.StudentId,
                score.SubjectId,
                score.SemesterId,
                _currentUser.UserId,
                _currentUser.IsInRole("Admin"),
                cancellationToken);

            if (!hasPermission)
            {
                _logger.Warning("User {UserName} (ID: {UserId}) attempted to delete score {ScoreId} without permission. Action Forbidden.",
                                currentUserName, _currentUser.UserId, request.Id);
                throw new ForbiddenException("You do not have permission to delete this score.");
            }

            // Store old values for audit
            var oldValues = new { score.StudentId, score.SubjectId, score.SemesterId, score.Value.Value };

            score.MarkAsDeleted(currentUserName); // Soft delete
            _scoreRepository.Delete(score); // Repository method usually just marks as deleted in ORM

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Audit Trail
            await _auditLogService.LogAuditAsync(
                score.Id,
                nameof(score),
                "Delete",
                oldValues,
                null, // No new values, just marked as deleted
                currentUserName,
                cancellationToken);

            _logger.Information("Score {ScoreId} soft-deleted successfully by {UserName}.", request.Id, currentUserName);
        }
    }
}