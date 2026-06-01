// QUAN-20260531-154643
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("subjects"); // snake_case cho PostgreSQL

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .HasColumnName("code")
                .HasMaxLength(20)
                .IsRequired();
            builder.HasIndex(x => x.Code).IsUnique().HasDatabaseName("ix_subjects_code"); // Unique Index for Code

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("ix_subjects_name"); // Unique Index for Name

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.Property(x => x.Credits)
                .HasColumnName("credits")
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .IsRequired();
            builder.HasIndex(x => x.IsActive).HasDatabaseName("ix_subjects_is_active"); // Index for IsActive

            // BaseEntity properties mapping
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(100).IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnName("last_modified_at"); // Mapping UpdatedAt to last_modified_at as per SRS
            builder.Property(x => x.UpdatedBy).HasColumnName("last_modified_by").HasMaxLength(100); // Mapping UpdatedBy to last_modified_by as per SRS
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired();

            // Optimistic Concurrency with xmin
            builder.Property<uint>("xmin") // xmin is a system column in PostgreSQL for optimistic concurrency
                   .HasColumnType("xid")
                   .ValueGeneratedOnAddOrUpdate()
                   .IsConcurrencyToken();

            builder.HasIndex(x => x.CreatedAt).IsDescending().HasDatabaseName("ix_subjects_created_at"); // Index for CreatedAt
        }
    }
}