// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Features.Scores.Queries; // For IStudentRepository
using ONENET.Domain.Entities;
using ONENET.Infrastructure.Persistence;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, ct);
        }
    }
}