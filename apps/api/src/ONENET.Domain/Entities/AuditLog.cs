using ONENET.Domain.Common;

namespace ONENET.Domain.Entities
{
    // Không kế thừa BaseEntity vì có cấu trúc riêng cho audit
    public class AuditLog
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string UserName { get; private set; }
        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
        public string ActionType { get; private set; }
        public string EntityName { get; private set; }
        public Guid EntityId { get; private set; }
        public string Changes { get; private set; } // Stored as JSONB in PostgreSQL

        // EF Core constructor
        private AuditLog() { }

        public AuditLog(string userName, string actionType, string entityName, Guid entityId, string changes)
        {
            UserName = userName;
            ActionType = actionType;
            EntityName = entityName;
            EntityId = entityId;
            Changes = changes;
        }
    }
}