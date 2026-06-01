// QUAN-20260531-154643
namespace ONENET.Application.Features.Scores.Services
{
    public interface IScorePermissionService
    {
        // BR04: Check if the current user (teacher or admin) has permission to manage (create/update/delete/view) a specific score record.
        Task<bool> CanManageScoreAsync(Guid studentId, Guid subjectId, Guid semesterId, string? userId, bool isAdmin, CancellationToken ct = default);

        // Potentially, for list queries, an additional method to filter IQueryable based on user's permissions
        // IQueryable<Score> ApplyListPermissionFilter(IQueryable<Score> query, string? userId, bool isAdmin);
    }
}