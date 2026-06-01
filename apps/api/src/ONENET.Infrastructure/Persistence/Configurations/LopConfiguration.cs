// QUAN-20260530-2301
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONENET.Domain.Entities;

namespace ONENET.Infrastructure.Persistence.Configurations;

public class LopConfiguration : IEntityTypeConfiguration<Lop>
{
    public void Configure(EntityTypeBuilder<Lop> builder)
    {
        builder.ToTable("lops"); // snake_case cho PostgreSQL

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid");

        builder.Property(x => x.TenLop)
            .HasColumnName("ten_lop")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.TenLop).IsUnique(); // Unique Index

        builder.Property(x => x.MoTa)
            .HasColumnName("mo_ta")
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);
        builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").IsRequired();

        // Seed Data for Lops
        builder.HasData(
            new Lop(Guid.Parse("b6f2d5e8-1a2b-4c3d-5e6f-7a8b9c0d1e2f"), "10A1", "Lớp 10 ban A1") { CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new Lop(Guid.Parse("c7g3e6f9-2b3c-5d4e-6f7a-8b9c0d1e2f3a"), "10A2", "Lớp 10 ban A2") { CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new Lop(Guid.Parse("d8h4f7g0-3c4d-6e5f-7a8b-9c0d1e2f3a4b"), "11B1", "Lớp 11 ban B1") { CreatedAt = DateTime.UtcNow, CreatedBy = "system" },
            new Lop(Guid.Parse("e9i5h8a1-4d5e-7f6a-8b9c-0d1e2f3a4b5c"), "12C", "Lớp 12 ban C") { CreatedAt = DateTime.UtcNow, CreatedBy = "system" }
        );
    }
}