using ONENET.Domain.Entities;
using System.Linq.Expressions;

namespace ONENET.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsWithCodeAsync(string code, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> ExistsWithNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task AddAsync(Subject subject, CancellationToken cancellationToken = default);
        void Update(Subject subject);
        void Delete(Subject subject); // For soft delete via IsDeleted/IsActive

        IQueryable<Subject> GetQueryable();
    }
}