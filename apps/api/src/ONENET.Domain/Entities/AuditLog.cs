// QUAN-20260531-154643
using Newtonsoft.Json; // For JSON serialization in audit logs

namespace ONENET.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public string EntityType { get; private set; } = string.Empty;
        public Guid EntityId { get; private set; }
        public string Action { get; private set; } = string.Empty; // e.g., "Create", "Update", "Delete"
        public string? OldValues { get; private set; } // JSON string of old state
        public string? NewValues { get; private set; } // JSON string of new state
        public string Actor { get; private set; } = string.Empty;
        public DateTime Timestamp { get; private set; }

        private AuditLog() { }

        public static AuditLog Create<T>(Guid entityId, string entityType, string action, T? oldValues, T? newValues, string actor)
        {
            return new AuditLog
            {
                Id = Guid.NewGuid(), // AuditLog itself is an entity, needs its own Id
                EntityType = entityType,
                EntityId = entityId,
                Action = action,
                OldValues = oldValues != null ? JsonConvert.SerializeObject(oldValues) : null,
                NewValues = newValues != null ? JsonConvert.SerializeObject(newValues) : null,
                Actor = actor,
                Timestamp = DateTime.UtcNow,
                // BaseEntity audit fields not directly used for AuditLog entity itself,
                // but its Id generation and CreatedAt default can be used if desired.
                // For audit logs, Timestamp is more specific.
            };
        }
    }
}