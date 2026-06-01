// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence; // For AppDbContext

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly AppDbContext _context;

        public ScoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Score?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            // .AsNoTracking() not needed here as entity might be updated/deleted
            return await _context.Scores.FindAsync(new object[] { id }, ct);
        }

        public async Task<bool> ExistsByUniqueKeysAsync(Guid studentId, Guid subjectId, Guid semesterId, CancellationToken ct = default)
        {
            return await _context.Scores
                .AsNoTracking() // Read-only query
                .AnyAsync(s => s.StudentId == studentId && s.SubjectId == subjectId && s.SemesterId == semesterId && !s.IsDeleted, ct);
        }

        public async Task AddAsync(Score score, CancellationToken ct = default)
        {
            await _context.Scores.AddAsync(score, ct);
        }

        public void Update(Score score)
        {
            _context.Scores.Update(score);
        }

        public void Delete(Score score)
        {
            // Soft delete is handled by setting IsDeleted flag on the entity
            // and the SaveChangesAsync interceptor. We just mark the entity as modified.
            _context.Scores.Update(score);
        }
    }
}