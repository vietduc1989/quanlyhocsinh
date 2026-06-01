// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    // Minimal configuration for external Subject entity to support FKs and queries
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("subjects"); // Assumed external table name

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id");
            builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            builder.HasIndex(s => s.Name).IsUnique().HasDatabaseName("ix_subjects_name");

            // BaseEntity audit fields
            builder.Property(s => s.CreatedAt).HasColumnName("created_at");
            builder.Property(s => s.CreatedBy).HasColumnName("created_by");
            builder.Property(s => s.UpdatedAt).HasColumnName("updated_at");
            builder.Property(s => s.UpdatedBy).HasColumnName("updated_by");
            builder.Property(s => s.IsDeleted).HasColumnName("is_deleted");
        }
    }
}