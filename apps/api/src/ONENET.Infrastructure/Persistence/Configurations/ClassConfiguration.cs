using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations;

public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Classes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ClassName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.SchoolYear)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.IsActive)
            .HasDefaultValue(true);
    }
}