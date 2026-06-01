<!-- QUAN-20260530-2301 -->
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsMaHocSinhUniqueAsync(string maHocSinh, Guid? studentId = null, CancellationToken ct = default)
        {
            if (studentId.HasValue)
            {
                return await _dbSet.AnyAsync(s => s.MaHocSinh == maHocSinh && s.Id != studentId.Value, ct);
            }
            return await _dbSet.AnyAsync(s => s.MaHocSinh == maHocSinh, ct);
        }

        // Placeholder for checking related data. In a real system, this would involve
        // checking other tables like Grades, Attendance, etc.
        public Task<bool> HasRelatedDataAsync(Guid studentId, CancellationToken ct = default)
        {
            // For now, assume no related data if we can't delete based on the spec requirement.
            // In a real application, you'd check tables like `Grades.Any(g => g.StudentId == studentId)`
            // Or `Attendance.Any(a => a.StudentId == studentId)`.
            // For this specific BRD, it's a simple placeholder.
            return Task.FromResult(false); // Currently no other entities related to Student defined in BRD.
                                         // If "related data" means logical dependency, this needs to be enhanced.
                                         // For now, it only blocks if there *was* actual DB related entities.
        }

        public IQueryable<Student> GetQueryableStudentsWithLopAndTrangThai()
        {
            return _dbSet
                .Include(s => s.Lop)
                .Include(s => s.TrangThai)
                .AsNoTracking();
        }

        public async Task<Student?> GetStudentDetailsAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(s => s.Lop)
                .Include(s => s.TrangThai)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, ct);
        }
    }
}