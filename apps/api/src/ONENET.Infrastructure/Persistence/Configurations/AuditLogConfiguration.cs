// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("audit_logs"); // snake_case

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("id"); // BaseEntity's Id is used.

            builder.Property(a => a.EntityType).HasColumnName("entity_type").HasMaxLength(50).IsRequired();
            builder.Property(a => a.EntityId).HasColumnName("entity_id").IsRequired();
            builder.Property(a => a.Action).HasColumnName("action").HasMaxLength(50).IsRequired();
            
            // Use jsonb for PostgreSQL for JSON columns
            builder.Property(a => a.OldValues).HasColumnName("old_values").HasColumnType("jsonb").IsRequired(false);
            builder.Property(a => a.NewValues).HasColumnName("new_values").HasColumnType("jsonb").IsRequired(false);
            
            builder.Property(a => a.Actor).HasColumnName("actor").HasMaxLength(255).IsRequired();
            builder.Property(a => a.Timestamp).HasColumnName("timestamp").IsRequired();

            // Indexes for querying audit logs
            builder.HasIndex(a => new { a.EntityType, a.EntityId }).HasDatabaseName("ix_audit_logs_entity_type_entity_id");
            builder.HasIndex(a => a.Actor).HasDatabaseName("ix_audit_logs_actor");
            builder.HasIndex(a => a.Timestamp).HasDatabaseName("ix_audit_logs_timestamp");

            // Exclude BaseEntity audit fields as AuditLog has its own Timestamp and Actor
            builder.Ignore(a => a.CreatedAt);
            builder.Ignore(a => a.CreatedBy);
            builder.Ignore(a => a.UpdatedAt);
            builder.Ignore(a => a.UpdatedBy);
            builder.Ignore(a => a.IsDeleted);
        }
    }
}