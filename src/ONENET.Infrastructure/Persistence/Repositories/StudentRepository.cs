using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Repositories;

namespace ONENET.Infrastructure.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .Include(s => s.Class)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Student?> GetByCodeAsync(string studentCode, CancellationToken cancellationToken = default)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.StudentCode == studentCode, cancellationToken);
    }

    public async Task<bool> IsCodeUniqueAsync(string studentCode, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        return !await _context.Students
            .AnyAsync(s => s.StudentCode == studentCode && s.Id != excludeId, cancellationToken);
    }

    public async Task<(IEnumerable<Student> Items, int TotalCount)> GetPagedListAsync(
        string? searchTerm, Guid? classId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Students
            .Include(s => s.Class)
            .AsNoTracking()
            .AsQueryable();

        // 1. Tìm kiếm theo tên hoặc mã học sinh
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim().ToLower();
            query = query.Where(s => s.FullName.ToLower().Contains(search) || s.StudentCode.ToLower().Contains(search));
        }

        // 2. Lọc theo lớp
        if (classId.HasValue && classId != Guid.Empty)
        {
            query = query.Where(s => s.ClassId == classId.Value);
        }

        // 3. Sắp xếp mặc định Tên từ A-Z
        query = query.OrderBy(s => s.FullName);

        var totalCount = await query.CountAsync(cancellationToken);

        // 4. Phân trang
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _context.Students.AddAsync(student, cancellationToken);
    }

    public void Update(Student student)
    {
        _context.Students.Update(student);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}