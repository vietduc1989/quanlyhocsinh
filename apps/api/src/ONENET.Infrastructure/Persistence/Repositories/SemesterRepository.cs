// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Features.Scores.Queries; // For ISemesterRepository
using ONENET.Domain.Entities;
using ONENET.Infrastructure.Persistence;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly AppDbContext _context;

        public SemesterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Semester?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Semesters.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, ct);
        }
    }
}