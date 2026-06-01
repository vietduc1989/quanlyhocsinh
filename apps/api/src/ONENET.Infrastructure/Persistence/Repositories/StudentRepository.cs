// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;

namespace ONENET.Infrastructure.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _dbContext;

    public StudentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Students
            .AsNoTracking()
            .Include(s => s.Lop)
            .Include(s => s.TrangThaiHocSinh)
            .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, ct);
    }

    public async Task<Student?> GetByMaHocSinhAsync(string maHocSinh, CancellationToken ct = default)
    {
        return await _dbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.MaHocSinh == maHocSinh && !s.IsDeleted, ct);
    }

    public async Task AddAsync(Student student, CancellationToken ct = default)
    {
        await _dbContext.Students.AddAsync(student, ct);
    }

    public void Update(Student student)
    {
        _dbContext.Students.Update(student);
    }

    public void Delete(Student student)
    {
        student.IsDeleted = true; // Soft delete
        _dbContext.Students.Update(student);
    }

    public async Task<bool> ExistsByMaHocSinhAsync(string maHocSinh, Guid? excludeId = null, CancellationToken ct = default)
    {
        return await _dbContext.Students
            .AnyAsync(s => s.MaHocSinh == maHocSinh && !s.IsDeleted && (excludeId == null || s.Id != excludeId.Value), ct);
    }

    // QUAN-20260530-2301-FR06: Placeholder for actual related data check.
    // In a real application, this would involve checking related tables (e.g., Grades, Attendance, etc.).
    public async Task<bool> HasRelatedDataAsync(Guid studentId, CancellationToken ct = default)
    {
        // Example: Check if student has any grades (assuming a Grades entity exists)
        // return await _dbContext.Grades.AnyAsync(g => g.StudentId == studentId && !g.IsDeleted, ct);

        // For now, returning false to allow deletion unless specific related entities are defined.
        // To demonstrate the 409 Conflict, I'll hardcode true for a specific ID or based on some condition.
        // For actual implementation, specific tables would be checked.
        return false; // Return false to allow deletion in initial implementation
    }
}