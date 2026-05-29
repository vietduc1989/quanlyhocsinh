/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Domain (Interfaces)
 */

using System.Threading;
using System.Threading.Tasks;
using ONENET.Domain.Entities;

namespace ONENET.Domain.Repositories
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog log, CancellationToken cancellationToken = default);
    }
}