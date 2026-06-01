// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _dbContext;

        public SubjectRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Subjects.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<bool> ExistsWithCodeAsync(string code, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            if (excludeId.HasValue)
            {
                return await _dbContext.Subjects.AnyAsync(s => s.Code == code && s.Id != excludeId.Value, cancellationToken);
            }
            return await _dbContext.Subjects.AnyAsync(s => s.Code == code, cancellationToken);
        }

        public async Task<bool> ExistsWithNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            if (excludeId.HasValue)
            {
                return await _dbContext.Subjects.AnyAsync(s => s.Name == name && s.Id != excludeId.Value, cancellationToken);
            }
            return await _dbContext.Subjects.AnyAsync(s => s.Name == name, cancellationToken);
        }

        public async Task AddAsync(Subject subject, CancellationToken cancellationToken = default)
        {
            await _dbContext.Subjects.AddAsync(subject, cancellationToken);
        }

        public void Update(Subject subject)
        {
            _dbContext.Subjects.Update(subject);
        }

        public void Delete(Subject subject)
        {
            // Soft delete is handled by setting IsDeleted = true (or IsActive = false) in the entity itself,
            // and the global query filter will exclude it.
            // This method is just a placeholder to mark for modification, if needed.
            // The Subject entity's SoftDelete method should be called before passing to this.
            _dbContext.Subjects.Update(subject);
        }

        public IQueryable<Subject> GetQueryable()
        {
            return _dbContext.Subjects;
        }
    }
}