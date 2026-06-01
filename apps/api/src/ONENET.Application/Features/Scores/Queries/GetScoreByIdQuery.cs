// QUAN-20260531-154643
using MediatR;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.Scores.DTOs;
using ONENET.Domain.Interfaces;
using Serilog;

namespace ONENET.Application.Features.Scores.Queries
{
    public record GetScoreByIdQuery(Guid Id) : IRequest<ScoreDto>;

    public class GetScoreByIdQueryHandler : IRequestHandler<GetScoreByIdQuery, ScoreDto>
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger _logger;
        private readonly IScorePermissionService _permissionService;
        private readonly IStudentRepository _studentRepository; // Assuming these exist for DTO population
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISemesterRepository _semesterRepository;

        public GetScoreByIdQueryHandler(
            IScoreRepository scoreRepository,
            ICurrentUser currentUser,
            ILogger logger,
            IScorePermissionService permissionService,
            IStudentRepository studentRepository,
            ISubjectRepository subjectRepository,
            ISemesterRepository semesterRepository)
        {
            _scoreRepository = scoreRepository;
            _currentUser = currentUser;
            _logger = logger;
            _permissionService = permissionService;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _semesterRepository = semesterRepository;
        }

        public async Task<ScoreDto> Handle(GetScoreByIdQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated)
            {
                _logger.Warning("Unauthorized attempt to get score detail. User not authenticated.");
                throw new ForbiddenException("You must be authenticated to view score details.");
            }

            var score = await _scoreRepository.GetByIdAsync(request.Id, cancellationToken);

            if (score == null || score.IsDeleted)
            {
                _logger.Warning("Score not found for detail query: {ScoreId} by user {UserName}.", request.Id, _currentUser.UserName);
                throw new NotFoundException($"Score with ID {request.Id} not found.");
            }

            // BR04: Check permission
            var hasPermission = await _permissionService.CanManageScoreAsync(
                score.StudentId,
                score.SubjectId,
                score.SemesterId,
                _currentUser.UserId,
                _currentUser.IsInRole("Admin"),
                cancellationToken);

            if (!hasPermission)
            {
                _logger.Warning("User {UserName} (ID: {UserId}) attempted to view score {ScoreId} without permission. Action Forbidden.",
                                _currentUser.UserName, _currentUser.UserId, request.Id);
                throw new ForbiddenException("You do not have permission to view this score.");
            }

            // Retrieve related entity details for DTO
            var student = await _studentRepository.GetByIdAsync(score.StudentId, cancellationToken);
            var subject = await _subjectRepository.GetByIdAsync(score.SubjectId, cancellationToken);
            var semester = await _semesterRepository.GetByIdAsync(score.SemesterId, cancellationToken);

            return new ScoreDto
            {
                Id = score.Id,
                StudentId = score.StudentId,
                StudentCode = student?.Code ?? "N/A",
                StudentFullName = student?.FullName ?? "N/A",
                SubjectId = score.SubjectId,
                SubjectName = subject?.Name ?? "N/A",
                SemesterId = score.SemesterId,
                SemesterName = semester?.Name ?? "N/A",
                SchoolYear = semester?.SchoolYear ?? "N/A",
                Value = score.Value.Value,
                CreatedBy = score.CreatedBy ?? "System",
                CreatedDate = score.CreatedAt,
                LastModifiedBy = score.UpdatedBy,
                LastModifiedDate = score.UpdatedAt
            };
        }
    }

    // Assuming these interfaces exist for external entities
    public interface IStudentRepository
    {
        Task<Domain.Entities.Student?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }

    public interface ISubjectRepository
    {
        Task<Domain.Entities.Subject?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }

    public interface ISemesterRepository
    {
        Task<Domain.Entities.Semester?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}