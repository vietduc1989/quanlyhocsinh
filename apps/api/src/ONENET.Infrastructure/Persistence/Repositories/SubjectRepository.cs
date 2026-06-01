// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Features.Scores.Queries; // For ISubjectRepository
using ONENET.Domain.Entities;
using ONENET.Infrastructure.Persistence;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subject?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, ct);
        }
    }
}