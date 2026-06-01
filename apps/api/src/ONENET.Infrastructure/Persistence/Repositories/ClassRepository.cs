// QUAN-20260530-2302
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Class?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Classes
                .AsNoTracking() // For read operations
                .Include(c => c.HomeroomTeacher)
                .Include(c => c.Students) // Include students to count them later
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<Class?> GetByCodeAsync(string classCode, CancellationToken ct = default)
        {
            return await _context.Classes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClassCode == classCode, ct);
        }

        public async Task<IReadOnlyList<Class>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Classes
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task AddAsync(Class classEntity, CancellationToken ct = default)
        {
            await _context.Classes.AddAsync(classEntity, ct);
        }

        public void Update(Class classEntity)
        {
            _context.Classes.Update(classEntity);
        }

        public void Delete(Class classEntity)
        {
            // AppDbContext's SaveChangesAsync handles soft delete based on BaseAuditableEntity
            _context.Classes.Remove(classEntity);
        }

        public async Task<int> GetStudentCountInClassAsync(Guid classId, CancellationToken ct = default)
        {
            return await _context.Students
                .AsNoTracking()
                .CountAsync(s => s.ClassId == classId && !s.IsDeleted, ct);
        }

        public async Task<bool> IsClassCodeUniqueAsync(string classCode, Guid? excludeId = null, CancellationToken ct = default)
        {
            if (excludeId.HasValue)
            {
                return !await _context.Classes.AnyAsync(c => c.ClassCode == classCode && c.Id != excludeId.Value, ct);
            }
            return !await _context.Classes.AnyAsync(c => c.ClassCode == classCode, ct);
        }
    }
}