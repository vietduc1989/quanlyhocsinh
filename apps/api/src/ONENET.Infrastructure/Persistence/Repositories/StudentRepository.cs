// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            return await _context.Students.FindAsync(new object[] { id }, ct);
        }

        public IQueryable<Student> GetAll()
        {
            return _context.Students;
        }

        public async Task AddAsync(Student student, CancellationToken ct = default)
        {
            await _context.Students.AddAsync(student, ct);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }

        public void Delete(Student student)
        {
            // Soft delete is handled at the application layer by setting IsDeleted = true
            // and then calling Update. This method is here for repository interface completeness
            // but its direct usage for hard delete is discouraged based on guidelines.
            // If hard delete was ever needed, this is where it would go.
            _context.Students.Remove(student);
        }

        public async Task<bool> ExistsByMaHocSinhAsync(string maHocSinh, Guid? excludeId = null, CancellationToken ct = default)
        {
            if (excludeId.HasValue)
            {
                return await _context.Students.AnyAsync(s => s.MaHocSinh == maHocSinh && s.Id != excludeId.Value, ct);
            }
            return await _context.Students.AnyAsync(s => s.MaHocSinh == maHocSinh, ct);
        }

        // QUAN-20260530-2301-FR06: Check for related data
        public async Task<bool> HasRelatedDataAsync(Guid studentId, CancellationToken ct = default)
        {
            // This is a placeholder check. Replace with actual related entity checks if/when those entities are implemented.
            // For example, if there were a Grades table:
            // return await _context.Grades.AnyAsync(g => g.StudentId == studentId, ct);
            // If there were a StudentAttendances table:
            // return await _context.StudentAttendances.AnyAsync(sa => sa.StudentId == studentId, ct);

            // Currently, there are no other tables defined that link directly to Student
            // apart from Lop and TrangThaiHocSinh, which are lookup tables.
            // For now, this will return false, but it's designed to be extended.
            // Assuming "DiemSo" and "LichSuHocTap" mentioned in SRS are not yet implemented entities.
            return false; 
        }
    }
}