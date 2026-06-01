// QUAN-20260531-154643
using ONENET.Domain.Entities;

namespace ONENET.Domain.Interfaces
{
    public interface IAuditLogService
    {
        Task LogAuditAsync<T>(Guid entityId, string entityType, string action, T? oldValues, T? newValues, string actor, CancellationToken ct = default);
    }
}