using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ONENET.Domain.Entities;

namespace ONENET.Domain.Repositories;

/// <summary>
/// Interface cung cấp các phương thức truy xuất dữ liệu chuyên biệt cho Student.
/// Tuân thủ quy tắc Dependency Inversion (SOLID).
/// </summary>
public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Student?> GetByCodeAsync(string studentCode, CancellationToken cancellationToken = default);
    Task<bool> IsCodeUniqueAsync(string studentCode, Guid? excludeId = null, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Student> Items, int TotalCount)> GetPagedListAsync(
        string? searchTerm, Guid? classId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(Student student, CancellationToken cancellationToken = default);
    void Update(Student student);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}