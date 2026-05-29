/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Domain (Entities)
 */

using System;

namespace ONENET.Domain.Entities
{
    public class AuditLog
    {
        public string LogId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; // CREATE, UPDATE, DELETE, IMPORT, EXPORT
        public string ObjectAffected { get; set; } = string.Empty; // StudentCode
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public string IpAddress { get; set; } = string.Empty;
        public string? OldValues { get; set; } // JSON format
        public string? NewValues { get; set; } // JSON format
    }
}