// QUAN-20260531-154643
using MediatR;
using ONENET.Application.Common.Interfaces;
using ONENET.Domain.Interfaces;
using ONENET.Application.Common.Exceptions;
using ONENET.Domain.ValueObjects;
using Serilog;

namespace ONENET.Application.Features.Scores.Commands
{
    public record UpdateScoreCommand(
        Guid Id,
        decimal Value
    ) : IRequest; // No return value for update

    public class UpdateScoreCommandHandler : IRequestHandler<UpdateScoreCommand>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger _logger;
        private readonly IScorePermissionService _permissionService;
        private readonly IAuditLogService _auditLogService;

        public UpdateScoreCommandHandler(
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

        public async Task Handle(UpdateScoreCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                _logger.Warning("Unauthorized attempt to update score. User not authenticated.");
                throw new ForbiddenException("You must be authenticated to update a score.");
            }

            var score = await _scoreRepository.GetByIdAsync(request.Id, cancellationToken);

            if (score == null || score.IsDeleted)
            {
                _logger.Warning("Score not found for update: {ScoreId} by user {UserName}.", request.Id, _currentUser.UserName);
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
                _logger.Warning("User {UserName} (ID: {UserId}) attempted to update score {ScoreId} without permission. Action Forbidden.",
                                currentUserName, _currentUser.UserId, request.Id);
                throw new ForbiddenException("You do not have permission to update this score.");
            }

            // Store old values for audit
            var oldValues = new { score.Value.Value };

            // BR01: ScoreValue handles range and decimal places.
            var newScoreValue = ScoreValue.FromDecimal(request.Value);

            score.UpdateValue(newScoreValue, currentUserName); // Use current user for UpdatedBy

            _scoreRepository.Update(score);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Audit Trail
            await _auditLogService.LogAuditAsync(
                score.Id,
                nameof(score),
                "Update",
                oldValues,
                new { score.Value.Value, score.UpdatedBy },
                currentUserName,
                cancellationToken);

            _logger.Information("Score {ScoreId} updated successfully to value {NewValue} by {UserName}.", request.Id, request.Value, currentUserName);
        }
    }
}