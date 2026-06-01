using ONENET.Domain.Entities;

namespace ONENET.Domain.Interfaces
{
    public interface IScoreRepository
    {
        Task<Score?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> ExistsByUniqueKeysAsync(Guid studentId, Guid subjectId, Guid semesterId, CancellationToken ct = default);
        Task AddAsync(Score score, CancellationToken ct = default);
        void Update(Score score);
        void Delete(Score score); // Soft delete
    }
}
