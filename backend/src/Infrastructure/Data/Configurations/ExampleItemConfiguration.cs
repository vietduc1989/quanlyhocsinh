using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ExampleItemConfiguration : IEntityTypeConfiguration<ExampleItem>
{
    public void Configure(EntityTypeBuilder<ExampleItem> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(1000);
    }
}