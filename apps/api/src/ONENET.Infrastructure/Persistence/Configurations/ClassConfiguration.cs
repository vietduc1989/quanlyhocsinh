// QUAN-20260530-2302
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("classes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ClassCode)
                .HasColumnName("class_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(c => c.ClassCode)
                .IsUnique();

            builder.Property(c => c.ClassName)
                .HasColumnName("class_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.SchoolYear)
                .HasColumnName("school_year")
                .HasMaxLength(9)
                .IsRequired();

            builder.Property(c => c.HomeroomTeacherId)
                .HasColumnName("homeroom_teacher_id")
                .IsRequired(false); // Nullable FK

            builder.Property(c => c.Version)
                .HasColumnName("version")
                .IsRowVersion(); // For optimistic concurrency

            // BaseAuditableEntity properties
            builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(c => c.CreatedBy).HasColumnName("created_by").HasMaxLength(256);
            builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
            builder.Property(c => c.UpdatedBy).HasColumnName("updated_by").HasMaxLength(256);
            builder.Property(c => c.IsDeleted).HasColumnName("is_deleted").IsRequired();

            // Relationships
            builder.HasOne(c => c.HomeroomTeacher)
                .WithMany(t => t.HomeroomClasses) // Assuming Teacher has ICollection<Class> HomeroomClasses
                .HasForeignKey(c => c.HomeroomTeacherId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a teacher assigned to a class
        }
    }
}