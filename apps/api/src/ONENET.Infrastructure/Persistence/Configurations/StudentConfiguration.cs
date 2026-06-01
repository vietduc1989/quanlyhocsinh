// QUAN-20260530-2302
// Assume StudentConfiguration.cs already exists, adding FK to Class
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("students"); // Example table name

            builder.HasKey(s => s.Id);

            builder.Property(s => s.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.ClassId)
                .HasColumnName("class_id")
                .IsRequired(false); // Nullable FK

            // BaseAuditableEntity properties
            builder.Property(s => s.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(s => s.CreatedBy).HasColumnName("created_by").HasMaxLength(256);
            builder.Property(s => s.UpdatedAt).HasColumnName("updated_at");
            builder.Property(s => s.UpdatedBy).HasColumnName("updated_by").HasMaxLength(256);
            builder.Property(s => s.IsDeleted).HasColumnName("is_deleted").IsRequired();

            // Relationships
            builder.HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a class that has students
            
            builder.HasIndex(s => s.ClassId); // Index for efficient lookups
        }
    }
}