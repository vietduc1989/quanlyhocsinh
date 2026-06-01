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
            builder.ToTable("audit_log"); // snake_case cho PostgreSQL

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(x => x.UserName)
                .HasColumnName("user_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .HasColumnName("timestamp")
                .IsRequired();
            builder.HasIndex(x => x.Timestamp).IsDescending().HasDatabaseName("ix_auditlogs_timestamp");

            builder.Property(x => x.ActionType)
                .HasColumnName("action_type")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.EntityName)
                .HasColumnName("entity_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.EntityId)
                .HasColumnName("entity_id")
                .IsRequired();
            builder.HasIndex(x => x.EntityId).HasDatabaseName("ix_auditlogs_entity_id");

            builder.Property(x => x.Changes)
                .HasColumnName("changes")
                .HasColumnType("jsonb") // PostgreSQL JSONB type
                .IsRequired();
        }
    }
}