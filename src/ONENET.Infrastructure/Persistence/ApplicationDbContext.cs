/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Infrastructure (Persistence)
 */

using Microsoft.EntityFrameworkCore;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("students");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
                entity.Property(e => e.StudentCode).HasColumnName("student_code").HasMaxLength(20).IsRequired();
                entity.HasIndex(e => e.StudentCode).IsUnique();
                entity.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth").IsRequired();
                entity.Property(e => e.Gender).HasColumnName("gender").HasMaxLength(10).IsRequired();
                entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(255);
                entity.Property(e => e.ParentPhone).HasColumnName("parent_phone").HasMaxLength(15).IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100);
                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Thêm index tối ưu hóa theo SAD
                entity.HasIndex(e => new { e.FullName, e.StudentCode }).HasDatabaseName("idx_students_search").HasFilter("is_deleted = false");
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("audit_logs");
                entity.HasKey(e => e.LogId);
                entity.Property(e => e.LogId).HasColumnName("log_id").HasMaxLength(50);
                entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Action).HasColumnName("action").HasMaxLength(20).IsRequired();
                entity.Property(e => e.ObjectAffected).HasColumnName("object_affected").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Timestamp).HasColumnName("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IpAddress).HasColumnName("ip_address").HasMaxLength(45).IsRequired();
                
                // Lưu JSONB trong PostgreSQL
                entity.Property(e => e.OldValues).HasColumnName("old_values").HasColumnType("jsonb");
                entity.Property(e => e.NewValues).HasColumnName("new_values").HasColumnType("jsonb");

                entity.HasIndex(e => e.Timestamp).HasDatabaseName("idx_audit_logs_timestamp");
            });
        }
    }
}