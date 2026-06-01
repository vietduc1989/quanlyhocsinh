// QUAN-20260531-154643
using ONENET.Domain.Interfaces;
using ONENET.Domain.Entities;
using ONENET.Infrastructure.Persistence.Repositories;
using Serilog;
using ONENET.Application.Common.Interfaces; // For IUnitOfWork

namespace ONENET.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public AuditLogService(IAuditLogRepository auditLogRepository, IUnitOfWork unitOfWork, ILogger logger)
        {
            _auditLogRepository = auditLogRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task LogAuditAsync<T>(Guid entityId, string entityType, string action, T? oldValues, T? newValues, string actor, CancellationToken ct = default)
        {
            try
            {
                var auditLog = AuditLog.Create(entityId, entityType, action, oldValues, newValues, actor);
                await _auditLogRepository.AddAsync(auditLog, ct);
                await _unitOfWork.SaveChangesAsync(ct); // Save audit log immediately or in a batched manner
                _logger.Information("Audit Logged: Entity {EntityType} {EntityId}, Action {Action} by {Actor}.", entityType, entityId, action, actor);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to log audit for Entity {EntityType} {EntityId}, Action {Action}.", entityType, entityId, action);
            }
        }
    }
}