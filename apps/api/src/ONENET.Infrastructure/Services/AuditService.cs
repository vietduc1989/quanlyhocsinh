// QUAN-20260531-154643
using Microsoft.Extensions.Logging;
using ONENET.Domain.Entities;
using ONENET.Domain.Interfaces;
using ONENET.Infrastructure.Persistence;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Infrastructure.Services
{
    public class AuditService : IAuditService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<AuditService> _logger;

        public AuditService(AppDbContext dbContext, ILogger<AuditService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task LogAsync<TEntity>(string actionType, TEntity entity, string userName, object? changes = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            try
            {
                var entityType = typeof(TEntity);
                var entityIdProperty = entityType.GetProperty("Id"); // Assuming entity has an 'Id' property
                Guid entityId = (Guid)(entityIdProperty?.GetValue(entity) ?? Guid.Empty);

                string changesJson = changes != null
                    ? JsonSerializer.Serialize(changes, new JsonSerializerOptions { WriteIndented = false, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull })
                    : JsonSerializer.Serialize(entity, new JsonSerializerOptions { WriteIndented = false, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull });

                var auditLog = new AuditLog(
                    userName,
                    actionType,
                    entityType.Name,
                    entityId,
                    changesJson
                );

                await _dbContext.AuditLogs.AddAsync(auditLog, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Audit Logged: Action={ActionType}, Entity={EntityName}, EntityId={EntityId}", actionType, entityType.Name, entityId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to log audit entry for entity {EntityType} with Action {ActionType}.", typeof(TEntity).Name, actionType);
            }
        }
    }
}