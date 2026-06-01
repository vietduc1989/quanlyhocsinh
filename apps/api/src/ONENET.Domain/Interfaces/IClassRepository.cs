using ONENET.Domain.Entities;

namespace ONENET.Domain.Interfaces
{
    public interface IClassRepository
    {
        Task<Class?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Class?> GetByCodeAsync(string classCode, CancellationToken ct = default);
        Task<IReadOnlyList<Class>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Class classEntity, CancellationToken ct = default);
        void Update(Class classEntity);
        void Delete(Class classEntity);
        Task<int> GetStudentCountInClassAsync(Guid classId, CancellationToken ct = default);
        Task<bool> IsClassCodeUniqueAsync(string classCode, Guid? excludeId = null, CancellationToken ct = default);
    }
}