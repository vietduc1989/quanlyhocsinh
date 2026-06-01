// QUAN-20260531-154643
using ONENET.Domain.Entities;
using ONENET.Infrastructure.Persistence;

namespace ONENET.Infrastructure.Persistence.Repositories
{
    public interface IAuditLogRepository // Define interface for repository for clean architecture
    {
        Task AddAsync(AuditLog auditLog, CancellationToken ct = default);
    }

    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuditLog auditLog, CancellationToken ct = default)
        {
            await _context.AuditLogs.AddAsync(auditLog, ct);
        }
    }
}