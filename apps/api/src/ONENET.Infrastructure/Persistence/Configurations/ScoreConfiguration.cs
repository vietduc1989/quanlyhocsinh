using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;
using ONENET.Domain.ValueObjects;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.ToTable("scores"); // snake_case as per guideline

            builder.HasKey(s => s.Id);
            // Id is handled by BaseEntity and EF default for Guid.

            builder.Property(s => s.StudentId).HasColumnName("student_id").IsRequired();
            builder.Property(s => s.SubjectId).HasColumnName("subject_id").IsRequired();
            builder.Property(s => s.SemesterId).HasColumnName("semester_id").IsRequired();

            // Value object configuration for ScoreValue
            builder.OwnsOne(s => s.Value, sv =>
            {
                sv.Property(p => p.Value)
                    .HasColumnName("value")
                    .HasColumnType("decimal(4,2)") // BR01: DECIMAL with 2 decimal places
                    .IsRequired();
            });

            // BaseEntity audit fields mapping
            builder.Property(s => s.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(s => s.CreatedBy).HasColumnName("created_by").HasMaxLength(255).IsRequired();
            builder.Property(s => s.UpdatedAt).HasColumnName("updated_at");
            builder.Property(s => s.UpdatedBy).HasColumnName("updated_by").HasMaxLength(255);
            builder.Property(s => s.IsDeleted).HasColumnName("is_deleted").IsRequired();
            
            // Relationships with external entities (Students, Subjects, Semesters)
            builder.HasOne<Student>()
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if referenced

            builder.HasOne<Subject>()
                .WithMany()
                .HasForeignKey(s => s.SubjectId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if referenced

            builder.HasOne<Semester>()
                .WithMany()
                .HasForeignKey(s => s.SemesterId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if referenced

            // BR02: Unique index for (StudentId, SubjectId, SemesterId)
            builder.HasIndex(s => new { s.StudentId, s.SubjectId, s.SemesterId })
                .IsUnique()
                .HasDatabaseName("ix_scores_unique_student_subject_semester"); // snake_case for index name

            // Other indexes for query optimization (FR06)
            builder.HasIndex(s => s.StudentId).HasDatabaseName("ix_scores_student_id");
            builder.HasIndex(s => s.SubjectId).HasDatabaseName("ix_scores_subject_id");
            builder.HasIndex(s => s.SemesterId).HasDatabaseName("ix_scores_semester_id");
            // builder.HasIndex(s => s.Value).HasDatabaseName("ix_scores_value"); // Can add if often filtered by value
        }
    }
}
