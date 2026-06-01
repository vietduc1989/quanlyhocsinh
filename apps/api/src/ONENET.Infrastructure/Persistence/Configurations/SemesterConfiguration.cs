using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations
{
    // Minimal configuration for external Semester entity to support FKs and queries
    public class SemesterConfiguration : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            builder.ToTable("semesters"); // Assumed external table name

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id");
            builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(s => s.SchoolYear).HasColumnName("school_year").HasMaxLength(10).IsRequired();

            // BaseEntity audit fields
            builder.Property(s => s.CreatedAt).HasColumnName("created_at");
            builder.Property(s => s.CreatedBy).HasColumnName("created_by");
            builder.Property(s => s.UpdatedAt).HasColumnName("updated_at");
            builder.Property(s => s.UpdatedBy).HasColumnName("updated_by");
            builder.Property(s => s.IsDeleted).HasColumnName("is_deleted");
        }
    }
}