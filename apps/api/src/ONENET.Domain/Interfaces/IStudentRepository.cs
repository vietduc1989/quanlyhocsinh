// QUAN-20260530-2301
using ONENET.Domain.Entities;

namespace ONENET.Domain.Interfaces;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Student?> GetByMaHocSinhAsync(string maHocSinh, CancellationToken ct = default);
    Task AddAsync(Student student, CancellationToken ct = default);
    void Update(Student student);
    void Delete(Student student);
    Task<bool> ExistsByMaHocSinhAsync(string maHocSinh, Guid? excludeId = null, CancellationToken ct = default);
    Task<bool> HasRelatedDataAsync(Guid studentId, CancellationToken ct = default); // For FR06
}